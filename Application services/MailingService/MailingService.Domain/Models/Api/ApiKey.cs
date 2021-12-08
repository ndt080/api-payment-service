using System;
using System.ComponentModel.DataAnnotations;

namespace MailingService.Domain.Models.Api
{
    public class ApiKey
    {
        [Key]
        public string Id  { get; set; }

        public string Key  { get; set; }
        public DateTime SubscriptionStart  { get; set; }
        public DateTime SubscriptionEnd  { get; set; }
    }
}