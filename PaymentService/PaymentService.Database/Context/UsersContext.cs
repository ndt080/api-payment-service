using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PaymentService.Database.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration _configuration;

        public UsersContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>().Key(m => m.Id);
            base.OnModelCreating(builder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("UsersDB"));
        }
    }
}