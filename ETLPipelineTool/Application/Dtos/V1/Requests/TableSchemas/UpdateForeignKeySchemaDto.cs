namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record UpdateForeignKeySchemaDto
{
  public Guid? Id { get; init; }
  public string ConstraintName { get; init; } = default!;
  public string PrincipalTable { get; init; } = default!;
  public ICollection<UpdateForeignKeyColumnDto> ForeignKeyColumns { get; init; } = [];
  public ICollection<UpdateForeignKeyPrincipalColumnDto> PrincipalColumns { get; init; } = [];
  public byte[]? RowVersion { get; init; }
}
