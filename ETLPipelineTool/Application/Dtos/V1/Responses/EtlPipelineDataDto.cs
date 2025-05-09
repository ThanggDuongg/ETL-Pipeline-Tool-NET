namespace ETLPipelineTool.Application.Dtos.V1.Responses
{
    public record EtlPipelineDataDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public PipelineSourceType SourceType { get; init; }
        public PipelineTargetType TargetType { get; init; }
        public string SourceConfigurationJson { get; init; } = default!;
        public string TargetConfigurationJson { get; init; } = default!;
        public bool IsActive { get; init; }
        public byte[] RowVersion { get; init; } = default!;

        public EtlPipelineDataDto() { }

        public EtlPipelineDataDto(
            Guid id,
            string name,
            string description,
            PipelineSourceType sourceType,
            PipelineTargetType targetType,
            string sourceConfigurationJson,
            string targetConfigurationJson,
            bool isActive,
            byte[] rowVersion
        )
        {
            Id = id;
            Name = name;
            Description = description;
            SourceType = sourceType;
            TargetType = targetType;
            SourceConfigurationJson = sourceConfigurationJson;
            TargetConfigurationJson = targetConfigurationJson;
            IsActive = isActive;
            RowVersion = rowVersion;
        }
    }
}
