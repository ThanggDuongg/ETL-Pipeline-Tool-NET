namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class CreateFieldMappingCommand(
        Guid etlPipelineId,
        int order,
        string sourceField,
        string targetField,
        string? transformExpression
    ) : IRequest<Guid>
    {
        public Guid EtlPipelineId { get; set; } = etlPipelineId;
        public int Order { get; set; } = order;
        public string SourceField { get; set; } = sourceField;
        public string TargetField { get; set; } = targetField;
        public string? TransformExpression { get; set; } = transformExpression;
    }
}
