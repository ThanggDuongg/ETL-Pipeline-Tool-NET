namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record UpdateTableSchemaDto
{
  public Guid Id { get; init; }
  public string TableName { get; init; } = default!;
  public ICollection<UpdateColumnSchemaDto> Columns { get; init; } = [];
  public ICollection<UpdateForeignKeySchemaDto> ForeignKeys { get; init; } = [];
  public byte[] RowVersion { get; init; } = default!;
}
