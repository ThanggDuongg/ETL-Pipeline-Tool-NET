namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class UpdateFieldMappingCommand(
        Guid id,
        int order,
        string sourceField,
        string targetField,
        string? transformExpression,
        byte[] rowVersion
    ) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
        public int Order { get; set; } = order;
        public string SourceField { get; set; } = sourceField;
        public string TargetField { get; set; } = targetField;
        public string? TransformExpression { get; set; } = transformExpression;
        public byte[] RowVersion { get; set; } = rowVersion;
    }
}
