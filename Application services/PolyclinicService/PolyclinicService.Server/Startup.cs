using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PolyclinicService.Database.Repository;
using PolyclinicService.Domain.Repository;
using PolyclinicService.Domain.Services.Access;
using PolyclinicService.Domain.Services.Polyclinic;
using PolyclinicService.Database.Context;


namespace PolyclinicService.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            //     .AddNewtonsoftJson(options =>
            //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // );
            //var con = Configuration.GetConnectionString("PolyclinicServiceContext");
            services.AddDbContext<VisitContext>();
            
            //services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IPolyclinicService, Domain.Services.Polyclinic.PolyclinicService>();
            services.AddScoped<IApiRepository, ApiRepository>();
            services.AddScoped<IPolyclinicRepository, PolyclinicRepository>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PolyclinicService.Server", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PolyclinicService.Server v1"));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}