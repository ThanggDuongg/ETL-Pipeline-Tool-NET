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

            app.UseSerilogRequestLogging();
            app.UseApiDocumentSupport(provider, env);
            app.UseCors();
            //app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<AntiforgeryMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
