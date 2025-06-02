namespace ETLPipelineTool.Application.Features.FieldMappings.Commands;

public class DeleteFieldMappingCommand(Guid id) : IRequest<Unit>
{
  public Guid Id { get; } = id;
}
