using System.Collections.Generic;

namespace PaymentService.Domain.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public List<SubscriptionInfo> Subscriptions { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Email = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            Subscriptions = user.Subscriptions;
        }
    }
}