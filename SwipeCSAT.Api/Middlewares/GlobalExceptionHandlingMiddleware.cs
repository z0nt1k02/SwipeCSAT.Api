using System.Diagnostics;
using System.Net;

namespace SwipeCSAT.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
        catch(Exception ex)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);
            var filename = frame?.GetFileName();
            var lineNumber = frame?.GetFileLineNumber();
            _logger.LogError($"Ошибка: {ex.Message}");
            _logger.LogError($"Файл: {filename},строка: {lineNumber}");
            _logger.LogError($"Стек вызовов: \n{ex.StackTrace}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync("Упс произошла ошибка");
        }
    }
}   