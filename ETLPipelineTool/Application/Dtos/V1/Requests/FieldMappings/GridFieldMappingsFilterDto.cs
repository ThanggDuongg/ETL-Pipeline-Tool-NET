namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings
{
    public record GridFieldMappingsFilterDto
    {
        public GridDataSourceDto GridDataSourceDto { get; init; } = new();
    }
}
