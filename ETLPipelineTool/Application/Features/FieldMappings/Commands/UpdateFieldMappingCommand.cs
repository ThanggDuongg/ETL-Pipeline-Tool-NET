namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class UpdateFieldMappingCommand(
        Guid id,
        int order,
        ICollection<string> sourceFields,
        string targetField,
        string transformRuleType,
        string? transformConfig,
        byte[] rowVersion
    ) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
        public int Order { get; set; } = order;
        public ICollection<string> SourceFields { get; set; } = sourceFields;
        public string TargetField { get; set; } = targetField;
        public string TransformRuleType { get; set; } = transformRuleType;
        public string? TransformConfig { get; set; } = transformConfig;
        public byte[] RowVersion { get; set; } = rowVersion;
    }
}
