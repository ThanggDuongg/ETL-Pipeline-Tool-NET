namespace ETLPipelineTool.Application.Dtos.V1.Requests.EtlPipelines
{
  public record CreateEtlPipelineDto(
    string Name,
    string Description,
    PipelineSourceType SourceType,
    PipelineTargetType TargetType,
    string SourceConfigurationJson,
    string TargetConfigurationJson,
    bool IsActive
  );
}
