namespace ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

public record ForeignKeySchemaDataDto
{
  public Guid Id { get; init; }
  public string ConstraintName { get; init; } = default!;
  public string PrincipalTable { get; init; } = default!;
  public ICollection<ForeignKeyColumnDataDto> ForeignKeyColumns { get; init; } = [];
  public ICollection<ForeignKeyPrincipalColumnDataDto> PrincipalColumns { get; init; } = [];
  public byte[] RowVersion { get; init; } = default!;

  public ForeignKeySchemaDataDto() { }

  public ForeignKeySchemaDataDto(
    Guid id,
    string constraintName,
    string principalTable,
    ICollection<ForeignKeyColumnDataDto> foreignKeyColumns,
    ICollection<ForeignKeyPrincipalColumnDataDto> principalColumns,
    byte[] rowVersion
  )
  {
    Id = id;
    ConstraintName = constraintName;
    PrincipalTable = principalTable;
    ForeignKeyColumns = foreignKeyColumns;
    PrincipalColumns = principalColumns;
    RowVersion = rowVersion;
  }
}
