using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentService.Domain.Models;

namespace PaymentService.Database.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration _configuration;

        public UsersContext(IConfiguration configuration) => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("UsersDB"));
        }
    }
}