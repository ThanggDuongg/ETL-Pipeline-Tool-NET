namespace ETLPipelineTool.Api.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseApplicationMiddlewares(
            this IApplicationBuilder app,
            IWebHostEnvironment env
        )
        {
            var provider =
                app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseApiDocumentSupport(provider, env);
            app.UseCors();
            //app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<AntiforgeryMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
