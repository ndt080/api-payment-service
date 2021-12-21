using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.IRepositories;
using PaymentService.Domain.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PaymentService.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserRepository _userRepository;

        public UserService(IJwtUtils jwtUtils, IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Login(UserRequest model, string ipAddress)
        {
            var user = _userRepository.GetByEmail(model.Email);

            if (user is null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new Exception("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            RemoveOldRefreshTokens(user);

            _userRepository.Update(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public AuthenticateResponse Register(User model, string ipAddress)
        {
            var user = _userRepository.Add(model);

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);
            RemoveOldRefreshTokens(user);

            _userRepository.Update(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress,
                    $"Attempted reuse of revoked ancestor token: {token}");
                _userRepository.Update(user);
            }

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            RemoveOldRefreshTokens(user);

            _userRepository.Update(user);

            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _userRepository.Update(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        // helper methods

        private User GetUserByRefreshToken(string token)
        {
            var user = _userRepository.GetByToken(token);
            return user ?? throw new Exception("Invalid token");
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void RemoveOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTtl) <= DateTime.UtcNow);
        }

        private static void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress,
            string reason)
        {
            if (string.IsNullOrEmpty(refreshToken.ReplacedByToken)) return;
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken is { IsActive: true })
                RevokeRefreshToken(childToken, ipAddress, reason);
            else
                RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        }

        private static void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null,
            string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}