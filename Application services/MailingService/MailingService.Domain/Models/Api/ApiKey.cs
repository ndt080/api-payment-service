using System;
using System.ComponentModel.DataAnnotations;

namespace MailingService.Domain.Models.Api
{
    public class ApiKey
    {
        [Key]
        private string Id;
        private string Key;
        private DateTime SubscriptionStart;
        private DateTime SubscriptionEnd;
    }
}