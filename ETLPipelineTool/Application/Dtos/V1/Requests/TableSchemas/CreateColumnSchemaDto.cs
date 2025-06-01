namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record CreateColumnSchemaDto(
  string ColumnName,
  string DataType,
  bool IsPrimaryKey,
  bool IsNullable,
  int? MaxLength
);
