namespace TaskManagerApp.Middleware;

public static class ExceptionHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
