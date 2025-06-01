namespace ETLPipelineTool.Api.Extensions
{
  public static class ApplicationServiceExtension
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddMediatR(configuration =>
      {
        configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      });
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

      return services;
    }
  }
}
