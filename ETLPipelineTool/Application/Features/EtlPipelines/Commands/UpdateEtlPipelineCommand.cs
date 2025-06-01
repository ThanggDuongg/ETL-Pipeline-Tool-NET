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
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public PipelineSourceType SourceType { get; set; } = sourceType;
    public PipelineTargetType TargetType { get; set; } = targetType;
    public string SourceConfigurationJson { get; set; } = sourceConfigurationJson;
    public string TargetConfigurationJson { get; set; } = targetConfigurationJson;
    public bool IsActive { get; set; } = isActive;
    public byte[] RowVersion { get; set; } = rowVersion;
  }
}
