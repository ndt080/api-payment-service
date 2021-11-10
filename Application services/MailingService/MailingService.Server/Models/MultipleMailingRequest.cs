using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MailingService.Server.Models
{
    public class MultipleMailingRequest
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}