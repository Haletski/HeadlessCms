using FluentValidation;
using Articles.WebAPI.Application.Common.Exceptions;
using Articles.WebAPI.Application.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Articles.WebAPI.Application.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;

                var errorDetails = new ErrorDetailsResource();

                switch (error)
                {
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        errorDetails.Messages.AddRange(e.Errors.Select(x => x.ErrorMessage));
                        break;

                    case NotFoundException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status404NotFound;
                        errorDetails.Messages.Add("Entity is not found");
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        errorDetails.Messages.Add("Unexpected error occured");
                        break;
                }

                var result = JsonSerializer.Serialize(errorDetails);

                await response.WriteAsync(result);
            }
        }
    }
}
