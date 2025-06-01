namespace ETLPipelineTool.Api.Extensions
{
  public static class ApiVersioningExtension
  {
    public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
    {
      services
        .AddApiVersioning(options =>
        {
          options.DefaultApiVersion = new ApiVersion(1, 0);
          options.AssumeDefaultVersionWhenUnspecified = true;
          options.ReportApiVersions = true;
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
          options.AssumeDefaultVersionWhenUnspecified = true;
          options.DefaultApiVersion = new ApiVersion(1, 0);
          options.GroupNameFormat = "'v'VVV";
          options.SubstituteApiVersionInUrl = true;
        });

      return services;
    }
  }
}
