namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class DeleteTableSchemaCommand(Guid Id) : IRequest<Unit>
{
  public Guid Id { get; } = Id;
}
