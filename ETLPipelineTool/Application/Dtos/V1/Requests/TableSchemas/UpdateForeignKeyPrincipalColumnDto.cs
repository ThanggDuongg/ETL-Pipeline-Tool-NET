namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record UpdateForeignKeyPrincipalColumnDto
{
  public Guid? Id { get; init; }
  public string PrincipalColumnName { get; init; } = default!;
  public byte[]? RowVersion { get; init; }
}
