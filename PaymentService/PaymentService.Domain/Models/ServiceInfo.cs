using System.Collections.Generic;

namespace PaymentService.Domain.Models
{
    public class ServiceInfo
    {
        public string Name { get; set; }
        public IEnumerable<string> Methods { get; set; }
        public double SubscriptionCost { get; set; }
        public int SubscriptionDuration { get; set; }
        public string SubscriptionCurrency { get; set; }
        public string Url { get; set; }
        public string AddKeyMethod { get; set; }
    }
}