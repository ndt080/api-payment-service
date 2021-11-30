using System;
using System.ComponentModel.DataAnnotations;

namespace MailingService.Server.Models
{
    public class ApiKey
    {
        private string Id;
        private string Key;
        private DateTime SubscriptionStart;
        private DateTime SubscriptionEnd;
    }
}