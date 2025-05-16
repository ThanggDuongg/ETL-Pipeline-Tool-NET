namespace ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings
{
    public record FieldMappingDataDto
    {
        public Guid Id { get; init; }
        public Guid EtlPipelineId { get; init; }
        public int Order { get; init; }
        public string SourceField { get; init; } = default!;
        public string TargetField { get; init; } = default!;
        public string? TransformExpression { get; init; }
        public byte[]? RowVersion { get; init; }

        public FieldMappingDataDto(
            Guid id,
            Guid etlPipelineId,
            int order,
            string sourceField,
            string targetField,
            string? transformExpression,
            byte[]? rowVersion
        )
        {
            Id = id;
            EtlPipelineId = etlPipelineId;
            Order = order;
            SourceField = sourceField;
            TargetField = targetField;
            TransformExpression = transformExpression;
            RowVersion = rowVersion;
        }

        public FieldMappingDataDto() { }
    }
}
