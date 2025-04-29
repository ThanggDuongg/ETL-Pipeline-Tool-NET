namespace ETLPipelineTool.Api.Middlewares
{
    public class RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger
    )
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(
                "Handling request: {Method} {Path}",
                context.Request.Method,
                context.Request.Path
            );
            await _next(context);
            _logger.LogInformation("Finished handling request.");
        }
    }
}
