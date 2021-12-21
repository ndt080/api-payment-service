using System.Text.Json.Serialization;

namespace PaymentService.Server.Models
{
    public class SubscriptionRequest
    {
        [JsonIgnore]public int Id { get; set; }
        public string ServiceName { get; set; }
        public double PaymentAmount { get; set; }
    }
}