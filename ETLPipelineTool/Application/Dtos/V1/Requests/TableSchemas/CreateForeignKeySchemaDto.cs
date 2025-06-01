namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record CreateForeignKeySchemaDto(
  string ConstraintName,
  string PrincipalTable,
  ICollection<CreateForeignKeyColumnDto> ForeignKeyColumns,
  ICollection<CreateForeignKeyPrincipalColumnDto> PrincipalColumns
);
