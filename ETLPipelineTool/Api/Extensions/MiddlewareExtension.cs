using ETLPipelineTool.Api.Configurations;
using Hangfire;

namespace ETLPipelineTool.Api.Extensions
{
  public static class MiddlewareExtension
  {
    public static IApplicationBuilder UseApplicationMiddlewares(
      this IApplicationBuilder app,
      IWebHostEnvironment env
    )
    {
      var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

      app.UseMiniProfiler();
      app.UseMiniLog(env.ContentRootPath);
      app.UseSerilogRequestLogging();
      app.UseApiDocumentSupport(provider, env);
      app.UseCors();
      app.UseSecurityHeadersMiddleware();
      app.UseMiddleware<AntiforgeryMiddleware>();
      app.UseMiddleware<RequestLoggingMiddleware>();
      app.UseMiddleware<ExceptionMiddleware>();
      app.UseCacheControlHeaderMiddleware();
      app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseHangfireDashboard(
        "/jobs",
        new DashboardOptions
        {
          Authorization = [new HangfireAuthorizationFilter()],
          DashboardTitle = "ETL Pipeline Jobs",
        }
      );

      return app;
    }
  }
}
