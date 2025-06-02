namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class DeleteTableSchemaCommandHandler(ITableSchemaRepository repository, IEtlContext context)
  : IRequestHandler<DeleteTableSchemaCommand, Unit>
{
  public async Task<Unit> Handle(
    DeleteTableSchemaCommand command,
    CancellationToken cancellationToken
  )
  {
    await repository.DeleteByIdAsync(command.Id, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
