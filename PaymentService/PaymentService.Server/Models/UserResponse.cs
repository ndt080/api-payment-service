namespace PaymentService.Server.Models
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Tokens Tokens { get; set; }
    }
}