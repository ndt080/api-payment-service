using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentService.Domain.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string Email { get; set; }

        public List<SubscriptionInfo> Subscriptions { get; set; }

        [JsonIgnore] public string PasswordHash { get; set; }

        [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; }
    }
}