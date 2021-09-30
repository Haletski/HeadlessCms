using Articles.WebAPI.Application.Resources;
using Articles.WebAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = new ContentResult()
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                ContentType = MediaTypeNames.Application.Json
            };

            var hasApiKey = context.HttpContext.Request.Query.TryGetValue(HeaderConstants.ApiKey, out var extractedApiKey);

            if (!hasApiKey)
            {
                result.Content = JsonSerializer.Serialize(new ErrorDetailsResource("API key is not provided"));
                context.Result = result;

                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>(HeaderConstants.ApiKey);

            var isValid = apiKey.Equals(extractedApiKey, StringComparison.InvariantCultureIgnoreCase);

            if (!isValid)
            {
                result.Content = JsonSerializer.Serialize(new ErrorDetailsResource("API key is not valid"));
                context.Result = result;

                return;
            }

            await next();
        }
    }
}
