namespace PaymentService.Server.Models
{
    public class SubscriptionRequest
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double PaymentAmount { get; set; }
    }
}