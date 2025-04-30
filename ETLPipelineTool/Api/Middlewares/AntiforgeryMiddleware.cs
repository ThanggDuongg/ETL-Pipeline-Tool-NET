namespace ETLPipelineTool.Api.Middlewares
{
    public class AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
        private readonly RequestDelegate _next = next;
        private readonly IAntiforgery _antiforgery = antiforgery;

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;

            if (
                HttpMethods.IsGet(context.Request.Method)
                || path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
            )
            {
                await _next(context);
                return;
            }

            try
            {
                await _antiforgery.ValidateRequestAsync(context);
                await _next(context);
            }
            catch (AntiforgeryValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid CSRF token.");
            }
        }
    }
}
