using System;

namespace PaymentService.Domain.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Tokens Tokens { get; set; }
    }
}