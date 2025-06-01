namespace ETLPipelineTool.Api.Configurations.Swagger
{
  public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
  {
    public void Configure(SwaggerGenOptions options)
    {
      foreach (var description in provider.ApiVersionDescriptions)
      {
        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
      }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
      var info = new OpenApiInfo
      {
        Title = "ETL Tool",
        Version = description.ApiVersion.ToString(),
        Description = "ETL Tool Description",
      };

      if (description.IsDeprecated)
      {
        info.Description += $"{Environment.NewLine} This API version has been deprecated.";
      }

      return info;
    }
  }
}
