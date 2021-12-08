using System;
using System.ComponentModel.DataAnnotations;

namespace MailingService.Server.Models
{
    public class ApiKeyRequest
    {
        public string ApiKey  { get; set; }
        public DateTime Start  { get; set; }
        public DateTime End  { get; set; }
    }
}