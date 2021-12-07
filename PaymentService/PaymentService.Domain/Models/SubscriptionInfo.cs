using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Domain.Models
{
    public class SubscriptionInfo
    {
        [Key] 
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ApiKey { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}