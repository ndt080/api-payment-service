using System;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.Models;
using System.Linq;
using PaymentService.Domain.IRepositories;
using RefreshToken = PaymentService.Domain.Models.RefreshToken;
using User = PaymentService.Domain.Models.User;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PaymentService.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(IJwtUtils jwtUtils, AppSettings appSettings, IUserRepository userRepository)
        {
            _jwtUtils = jwtUtils;
            _appSettings = appSettings;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Login(UserRequest model, string ipAddress)
        {
            var user = _userRepository.GetByEmail(model.Email);

            // validate
            if (user is null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new Exception("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            RemoveOldRefreshTokens(user);

            // save changes to db
            _userRepository.Update(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public void Register(User user)
        {
            _userRepository.Add(user);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _userRepository.Update(user);
            }

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            RemoveOldRefreshTokens(user);

            // save changes to db
            _userRepository.Update(user);

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            // revoke token and save
            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _userRepository.Update(user);
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

        private void RemoveOldRefreshTokens(User user) =>
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTtl) <= DateTime.UtcNow);

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