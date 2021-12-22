using System;
using System.ComponentModel.DataAnnotations;

namespace PolyclinicService.Domain.Models.Api
{
    public class ApiKey
    {
        [Key]
        public int Id  { get; set; }

        public string Key  { get; set; }
        public DateTime SubscriptionStart  { get; set; }
        public DateTime SubscriptionEnd  { get; set; }
    }
}