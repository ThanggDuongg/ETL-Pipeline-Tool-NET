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
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            try
            {
                _logger.LogInformation(
                    "Started request: Method={Method}, Path={Path}, QueryString={QueryString}",
                    request.Method,
                    request.Path,
                    request.QueryString
                );

                await _next(context);

                stopwatch.Stop();
                _logger.LogInformation(
                    "Finished request: Method={Method}, Path={Path}, QueryString={QueryString}, Status=Success, SpentTime={SpentTime}ms",
                    request.Method,
                    request.Path,
                    request.QueryString,
                    stopwatch.ElapsedMilliseconds
                );
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(
                    ex,
                    "Finished request: Method={Method}, Path={Path}, QueryString={QueryString}, Status=Failure, SpentTime={SpentTime}ms",
                    request.Method,
                    request.Path,
                    request.QueryString,
                    stopwatch.ElapsedMilliseconds
                );
                throw;
            }
        }
    }
}
