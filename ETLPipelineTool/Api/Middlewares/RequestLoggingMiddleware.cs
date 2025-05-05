namespace ETLPipelineTool.Api.Middlewares
{
    public class RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger
    )
    {
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            try
            {
                logger.LogInformation(
                    "Started request: Method={Method}, Path={Path}, QueryString={QueryString}",
                    request.Method,
                    request.Path,
                    request.QueryString
                );

                await next(context);

                stopwatch.Stop();
                logger.LogInformation(
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
                logger.LogError(
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
