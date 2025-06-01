namespace ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

public record TableSchemaDataDto
{
  public Guid Id { get; init; }
  public Guid EtlPipelineId { get; init; }
  public string TableName { get; init; } = default!;
  public ICollection<ColumnSchemaDataDto> Columns { get; init; } = [];
  public ICollection<ForeignKeySchemaDataDto> ForeignKeys { get; init; } = [];
  public byte[]? RowVersion { get; init; }

  public TableSchemaDataDto() { }

  public TableSchemaDataDto(
    Guid id,
    Guid etlPipelineId,
    string tableName,
    ICollection<ColumnSchemaDataDto> columns,
    ICollection<ForeignKeySchemaDataDto> foreignKeys,
    byte[]? rowVersion
  )
  {
    Id = id;
    EtlPipelineId = etlPipelineId;
    TableName = tableName;
    Columns = columns;
    ForeignKeys = foreignKeys;
    RowVersion = rowVersion;
  }
}
