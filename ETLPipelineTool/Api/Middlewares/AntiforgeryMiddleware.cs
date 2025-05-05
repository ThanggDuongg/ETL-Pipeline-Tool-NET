namespace ETLPipelineTool.Api.Middlewares
{
    public class AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            var hasIgnoreAntiForgeryAttribute =
                context
                    .Features.Get<IEndpointFeature>()
                    ?.Endpoint?.Metadata.Any(m => m is IgnoreAntiforgeryTokenAttribute) ?? false;

            if (
                HttpMethods.IsGet(context.Request.Method)
                || path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
                || hasIgnoreAntiForgeryAttribute
            )
            {
                await next(context);
                return;
            }

            try
            {
                await antiforgery.ValidateRequestAsync(context);
                await next(context);
            }
            catch (AntiforgeryValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid CSRF token.");
            }
        }
    }
}
