namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record GridTableSchemasFilterDto
{
  public GridDataSourceDto GridDataSourceDto { get; init; } = new();
  public Guid? EtlPipelineId { get; init; }
}
