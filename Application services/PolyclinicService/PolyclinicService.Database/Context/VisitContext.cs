using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PolyclinicService.Domain.Models.Api;
using PolyclinicService.Domain.Models.Polyclinic;

namespace PolyclinicService.Database.Context
{
    public sealed class VisitContext : DbContext
    {
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Visit> Visits { get; set; }
        
        private readonly IConfiguration _configuration;

        //public VisitContext(DbContextOptions<VisitContext> options)
        //    : base(options)
        //{
        //    Database.EnsureCreated();
        //}
        //
        //public VisitContext()
        //{
        //    //           _configuration = configuration;
        //    Database.EnsureCreated();
        //}
        public VisitContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("ServiceDB"));
        }
    }
}