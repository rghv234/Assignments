using EasypayBackend.Data;
using EasypayBackend.Models;
using EasypayBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EasypayBackend.Middleware
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLogMiddleware> _logger;

        public AuditLogMiddleware(RequestDelegate next, ILogger<AuditLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAuditLogRepository auditLogRepository)
        {
            if (context.User.Identity?.IsAuthenticated == true && context.Request.Method != "GET")
            {
                try
                {
                    var userId = int.Parse(context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                    var auditLog = new AuditLog
                    {
                        UserId = userId,
                        Action = $"{context.Request.Method} {context.Request.Path}",
                        Timestamp = DateTime.UtcNow,
                        Details = $"User {userId} performed {context.Request.Method} on {context.Request.Path}"
                    };

                    await auditLogRepository.AddAsync(auditLog);
                    await auditLogRepository.SaveChangesAsync();

                    _logger.LogInformation("Audit Log: {Action} by User {UserId}", auditLog.Action, userId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save audit log for {Method} {Path}", context.Request.Method, context.Request.Path);
                }
            }

            await _next(context);
        }
    }

    public static class AuditLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditLogMiddleware>();
        }
    }
}