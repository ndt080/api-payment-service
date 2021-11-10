﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MailingService.Server.Models
{
    public class OneMailingRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}