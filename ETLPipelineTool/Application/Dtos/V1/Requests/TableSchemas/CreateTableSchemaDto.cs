namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record CreateTableSchemaDto(
  Guid EtlPipelineId,
  string TableName,
  ICollection<CreateColumnSchemaDto> Columns,
  ICollection<CreateForeignKeySchemaDto> ForeignKeys
);
