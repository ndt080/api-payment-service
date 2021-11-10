using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MailingService.Domain.Models.Mailing
{
    public class Email
    {
        public List<string> Addresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }  
    }
}