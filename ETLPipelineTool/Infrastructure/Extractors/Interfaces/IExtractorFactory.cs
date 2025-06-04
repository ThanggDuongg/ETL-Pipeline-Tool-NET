namespace ETLPipelineTool.Infrastructure.Extractors.Interfaces
{
  public interface IExtractorFactory
  {
    IExtractor Create(PipelineSourceType type);
    ISchemaExtractor CreateSchemaExtractor(PipelineSourceType type);
  }
}
