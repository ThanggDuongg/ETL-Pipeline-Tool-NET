namespace ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

public record ColumnSchemaDataDto
{
  public Guid Id { get; init; }
  public string ColumnName { get; init; } = default!;
  public string DataType { get; init; } = default!;
  public bool IsPrimaryKey { get; init; }
  public bool IsNullable { get; init; }
  public int? MaxLength { get; init; }
  public byte[]? RowVersion { get; init; }

  public ColumnSchemaDataDto() { }

  public ColumnSchemaDataDto(
    Guid id,
    string columnName,
    string dataType,
    bool isPrimaryKey,
    bool isNullable,
    int? maxLength,
    byte[]? rowVersion
  )
  {
    Id = id;
    ColumnName = columnName;
    DataType = dataType;
    IsPrimaryKey = isPrimaryKey;
    IsNullable = isNullable;
    MaxLength = maxLength;
    RowVersion = rowVersion;
  }
}
