namespace ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

public record ForeignKeyPrincipalColumnDataDto
{
  public Guid Id { get; init; }
  public string PrincipalColumnName { get; init; } = default!;

  public ForeignKeyPrincipalColumnDataDto() { }

  public ForeignKeyPrincipalColumnDataDto(Guid id, string principalColumnName)
  {
    Id = id;
    PrincipalColumnName = principalColumnName;
  }
}
