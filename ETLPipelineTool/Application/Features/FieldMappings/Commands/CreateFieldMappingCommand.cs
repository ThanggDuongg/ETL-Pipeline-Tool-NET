namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class CreateFieldMappingCommand(
        Guid etlPipelineId,
        int order,
        ICollection<string> sourceFields,
        string targetField,
        string transformRuleType,
        string? transformConfig
    ) : IRequest<Guid>
    {
        public Guid EtlPipelineId { get; set; } = etlPipelineId;
        public int Order { get; set; } = order;
        public ICollection<string> SourceFields { get; set; } = sourceFields;
        public string TargetField { get; set; } = targetField;
        public string TransformRuleType { get; set; } = transformRuleType;
        public string? TransformConfig { get; set; } = transformConfig;
    }
}
