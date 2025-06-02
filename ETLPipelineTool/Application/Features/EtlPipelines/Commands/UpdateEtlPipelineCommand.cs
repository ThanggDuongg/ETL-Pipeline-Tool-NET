namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class UpdateEtlPipelineCommand(
    Guid id,
    string name,
    string description,
    PipelineSourceType sourceType,
    PipelineTargetType targetType,
    string sourceConfigurationJson,
    string targetConfigurationJson,
    bool isActive,
    byte[] rowVersion
  ) : IRequest<Unit>
  {
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public PipelineSourceType SourceType { get; } = sourceType;
    public PipelineTargetType TargetType { get; } = targetType;
    public string SourceConfigurationJson { get; } = sourceConfigurationJson;
    public string TargetConfigurationJson { get; } = targetConfigurationJson;
    public bool IsActive { get; } = isActive;
    public byte[] RowVersion { get; set; } = rowVersion;
  }
}
