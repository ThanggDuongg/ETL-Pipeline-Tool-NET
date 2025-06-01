namespace ETLPipelineTool.Api.Extensions
{
  public static class HealthCheckExtension
  {
    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services)
    {
      services.AddHealthChecks();
      return services;
    }

    public static IEndpointRouteBuilder MapApplicationHealthChecks(
      this IEndpointRouteBuilder endpoint
    )
    {
      endpoint.MapHealthChecks("/health");
      return endpoint;
    }
  }
}
