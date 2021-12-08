using System.Collections.Generic;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public interface IUserService
    {
        AuthenticateResponse Register(User model, string ipAddress);
        AuthenticateResponse Login(UserRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);

        IEnumerable<User> GetAllUsers();
    }
}