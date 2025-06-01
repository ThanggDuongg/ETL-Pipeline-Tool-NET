namespace ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

public record ForeignKeyColumnDataDto
{
  public Guid Id { get; init; }
  public string ColumnName { get; init; } = default!;

  public ForeignKeyColumnDataDto() { }

  public ForeignKeyColumnDataDto(Guid id, string columnName)
  {
    Id = id;
    ColumnName = columnName;
  }
}
