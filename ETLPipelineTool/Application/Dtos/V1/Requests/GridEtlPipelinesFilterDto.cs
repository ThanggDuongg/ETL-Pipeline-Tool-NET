namespace ETLPipelineTool.Application.Dtos.V1.Requests
{
    public record GridEtlPipelinesFilterDto
    {
        public bool? IsActive { get; init; }
        public GridDataSourceDto GridDataSourceDto { get; init; } = new();
    }
}
