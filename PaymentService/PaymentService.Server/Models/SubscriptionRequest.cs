namespace PaymentService.Server.Models
{
    public class SubscriptionRequest
    {
        public string ServiceName { get; set; }
        public double PaymentAmount { get; set; }
    }
}