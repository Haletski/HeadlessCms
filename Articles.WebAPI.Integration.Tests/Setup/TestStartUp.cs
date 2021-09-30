using Articles.WebAPI.Application.Common.Behaviours;
using Articles.WebAPI.Application.Middlewares;
using Articles.WebAPI.Infrastructure.DataLayer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Articles.WebAPI.Integration.Tests
{
    public class TestStartUp
    {
        public TestStartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.Load("Articles.WebAPI");

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("ApplicationConnectionString");

                options.UseInMemoryDatabase("HeadlessCMSDb");

            }, ServiceLifetime.Scoped);

            services.AddControllers()
                .AddApplicationPart(assembly)
                .AddControllersAsServices();

            services.AddDbContext<ApplicationDbContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}