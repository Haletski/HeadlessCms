using Articles.WebAPI.Application.Attributes;
using Articles.WebAPI.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Articles.WebAPI.Application.Utilities
{
    public class ApiKeySecurityRequirement : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attr = context.MethodInfo.GetCustomAttribute<ApiKeyAttribute>();

            if (attr == null)
            {
                return;
            }

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = HeaderConstants.ApiKey 
                        }
                    },
                    new string[] { }
                }
            });
        }
    }
}
