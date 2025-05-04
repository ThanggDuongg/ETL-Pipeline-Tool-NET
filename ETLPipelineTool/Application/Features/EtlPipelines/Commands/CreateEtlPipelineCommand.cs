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
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public PipelineSourceType SourceType { get; set; } = sourceType;
        public PipelineTargetType TargetType { get; set; } = targetType;
        public string SourceConfigurationJson { get; set; } = sourceConfigurationJson;
        public string TargetConfigurationJson { get; set; } = targetConfigurationJson;
        public bool IsActive { get; set; } = isActive;
    }
}
