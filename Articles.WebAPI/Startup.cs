using Articles.WebAPI.Application.Middlewares;
using FluentValidation;
using Articles.WebAPI.Application.Common.Behaviours;
using Articles.WebAPI.Application.Utilities;
using Articles.WebAPI.Infrastructure.DataLayer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Articles.WebAPI.Constants;

namespace Articles.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters();

            services.AddSwaggerGen(opt =>
            {
                opt.DescribeAllParametersInCamelCase();
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Articles API",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Pavel Haletski",
                        Email = "haletski.p@gmail.com"
                    }
                });

                opt.AddSecurityDefinition(HeaderConstants.ApiKey, new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Query,
                    Name = HeaderConstants.ApiKey,
                    Description = "Authentication token for accessing API",
                });

                opt.OperationFilter<ApiKeySecurityRequirement>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            var executingAssembly = Assembly.GetExecutingAssembly();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(executingAssembly);
            services.AddAutoMapper(executingAssembly);
            services.AddValidatorsFromAssembly(executingAssembly);

            services.AddDbContext<ApplicationDbContext>(options =>
            { 
                var connectionString = Configuration.GetConnectionString("ApplicationConnectionString");

                options.UseSqlServer(connectionString, opt =>
                {
                    opt.CommandTimeout(240);
                    opt.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10), null);
                });

            }, ServiceLifetime.Scoped); // Share context between request pipeline

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Api Key";
                options.DefaultChallengeScheme = "Api Key";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
