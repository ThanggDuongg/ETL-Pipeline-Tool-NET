namespace ETLPipelineTool.Application.Features.FieldMappings.Commands;

public class DeleteFieldMappingCommandHandler(
  IFieldMappingRepository repository,
  IEtlContext context
) : IRequestHandler<DeleteFieldMappingCommand, Unit>
{
  public async Task<Unit> Handle(
    DeleteFieldMappingCommand command,
    CancellationToken cancellationToken
  )
  {
    await repository.DeleteByIdAsync(command.Id, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
