namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record UpdateForeignKeyColumnDto
{
  public Guid? Id { get; init; }
  public string ColumnName { get; init; } = default!;
  public byte[]? RowVersion { get; init; }
}
