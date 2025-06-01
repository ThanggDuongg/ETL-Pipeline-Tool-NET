namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record UpdateColumnSchemaDto
{
  public Guid? Id { get; init; }
  public string ColumnName { get; init; } = default!;
  public string DataType { get; init; } = default!;
  public bool IsPrimaryKey { get; init; }
  public bool IsNullable { get; init; }
  public int? MaxLength { get; init; }
  public byte[]? RowVersion { get; init; }
}
