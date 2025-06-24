using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EasypayBackend.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Fallback logging since log4net isn't working
                var error = $"EXCEPTION: {ex.Message}\n{ex.StackTrace}";
                Console.WriteLine(error);
                await File.AppendAllTextAsync("emergency_error.txt", error + Environment.NewLine);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = System.Text.Json.JsonSerializer.Serialize(new
                {
                    error = ex.Message,
                    stack = ex.StackTrace
                });

                await context.Response.WriteAsync(responseJson);
            }
        }

    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}