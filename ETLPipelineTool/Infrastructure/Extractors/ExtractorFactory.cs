using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Infrastructure.Extractors
{
  public class ExtractorFactory(IServiceProvider serviceProvider) : IExtractorFactory
  {
    public IExtractor Create(PipelineSourceType type) =>
      type switch
      {
        PipelineSourceType.MssqlDatabase => serviceProvider.GetRequiredService<MssqlExtractor>(),
        _ => throw new NotSupportedException($"Unsupported source {type}"),
      };

    public ISchemaExtractor CreateSchemaExtractor(PipelineSourceType type) =>
      type switch
      {
        PipelineSourceType.MssqlDatabase =>
          serviceProvider.GetRequiredService<MssqlSchemaExtractor>(),
        _ => throw new NotSupportedException(
          $"Source type {type} does not support schema extraction"
        ),
      };
  }
}
