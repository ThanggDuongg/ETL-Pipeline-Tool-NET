namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class CreateEtlPipelineCommand(
    string name,
    string description,
    PipelineSourceType sourceType,
    PipelineTargetType targetType,
    string sourceConfigurationJson,
    string targetConfigurationJson,
    bool isActive
  ) : IRequest<Guid>
  {
    public string Name { get; } = name;
    public string Description { get; } = description;
    public PipelineSourceType SourceType { get; } = sourceType;
    public PipelineTargetType TargetType { get; } = targetType;
    public string SourceConfigurationJson { get; } = sourceConfigurationJson;
    public string TargetConfigurationJson { get; } = targetConfigurationJson;
    public bool IsActive { get; } = isActive;
  }
}
