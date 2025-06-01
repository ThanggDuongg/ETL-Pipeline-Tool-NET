namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class DeleteTableSchemaCommandHandler : IRequestHandler<DeleteTableSchemaCommand, Unit>
{
  private readonly ITableSchemaRepository _repository;
  private readonly IEtlContext _context;

  public DeleteTableSchemaCommandHandler(ITableSchemaRepository repository, IEtlContext context)
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Unit> Handle(
    DeleteTableSchemaCommand command,
    CancellationToken cancellationToken
  )
  {
    await _repository.DeleteByIdAsync(command.Id, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
