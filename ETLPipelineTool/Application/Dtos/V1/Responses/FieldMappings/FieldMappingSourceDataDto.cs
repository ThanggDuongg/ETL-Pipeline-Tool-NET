namespace ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings
{
    public record FieldMappingSourceDataDto
    {
        public Guid Id { get; init; }
        public int Order { get; init; }
        public string SourceField { get; init; } = default!;
        public byte[]? RowVersion { get; init; }

        public FieldMappingSourceDataDto(Guid id, int order, string sourceField, byte[]? rowVersion)
        {
            Id = id;
            Order = order;
            SourceField = sourceField;
            RowVersion = rowVersion;
        }

        public FieldMappingSourceDataDto() { }
    }
}
