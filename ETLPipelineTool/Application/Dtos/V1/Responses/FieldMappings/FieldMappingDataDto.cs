namespace ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings
{
    public record FieldMappingDataDto
    {
        public Guid Id { get; init; }
        public Guid EtlPipelineId { get; init; }
        public int Order { get; init; }
        public ICollection<string> SourceFields { get; set; } = [];
        public string TargetField { get; init; } = default!;
        public string TransformRuleType { get; set; } = "Identity";
        public string? TransformConfig { get; set; }
        public byte[]? RowVersion { get; init; }

        public FieldMappingDataDto(
            Guid id,
            Guid etlPipelineId,
            int order,
            ICollection<string> sourceFields,
            string targetField,
            string transformRuleType,
            string? transformConfig,
            byte[]? rowVersion
        )
        {
            Id = id;
            EtlPipelineId = etlPipelineId;
            Order = order;
            SourceFields = sourceFields;
            TargetField = targetField;
            TransformRuleType = transformRuleType;
            TransformConfig = transformConfig;
            RowVersion = rowVersion;
        }

        public FieldMappingDataDto() { }
    }
}
