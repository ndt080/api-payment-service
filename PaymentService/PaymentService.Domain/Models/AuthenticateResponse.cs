using System.Text.Json.Serialization;

namespace PaymentService.Domain.Models
{
    public class AuthenticateResponse
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore] public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Email = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}