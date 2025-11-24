using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApp.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Continue through pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            _ => (int)HttpStatusCode.InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode == 500
                ? "An unexpected error occurred."
                : ex.Message,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
    }
}
