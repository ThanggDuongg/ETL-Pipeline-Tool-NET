namespace ETLPipelineTool.Api.Extensions
{
  public static class ApiDocumentExtension
  {
    public static IServiceCollection AddApiDocumentSupport(this IServiceCollection services)
    {
      // TODO: Fix - Display versioning options
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
      services.AddSwaggerGen(options =>
      {
        options.OperationFilter<SwaggerDefaultValues>();
      });
      return services;
    }

    public static IApplicationBuilder UseApiDocumentSupport(
      this IApplicationBuilder applicationBuilder,
      IApiVersionDescriptionProvider provider,
      IWebHostEnvironment env
    )
    {
      if (env.IsDevelopment())
      {
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(options =>
        {
          options.RoutePrefix = "swagger";
          foreach (var groupName in provider.ApiVersionDescriptions.Select(x => x.GroupName))
          {
            options.SwaggerEndpoint(
              $"{groupName}/swagger.json",
              $"API {groupName.ToUpperInvariant()}"
            );
          }
        });
      }

      return applicationBuilder;
    }
  }
}
