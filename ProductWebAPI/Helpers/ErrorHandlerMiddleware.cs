using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProductWebAPI.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ineternal Error");



            httpContext.Response.StatusCode = 500;
                
            await httpContext.Response.WriteAsJsonAsync(new {e = "internal error"});
        }

    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
