using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                if (ex is ModelValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var response = new { ex.Message, validationException.Errors };
                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
                if (ex is DbUpdateConcurrencyException)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    return;
                }
                if (ex is BusinessException exception)
                {
                    context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    await context.Response.WriteAsJsonAsync(
                        new BusinessExceptionDataDto(exception.Message)
                    );
                    return;
                }

                await context.Response.WriteAsJsonAsync(
                    new { Message = "An unexpected error occurred." }
                );
            }
        }
    }
}
