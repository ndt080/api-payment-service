using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public interface IUserService
    {
        void Register(User model);
        AuthenticateResponse Login(UserRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
    }
}