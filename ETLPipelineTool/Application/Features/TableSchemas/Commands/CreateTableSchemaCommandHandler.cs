using ETLPipelineTool.Application.Features.TableSchemas.Mappings;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class CreateTableSchemaCommandHandler : IRequestHandler<CreateTableSchemaCommand, Unit>
{
  private readonly ITableSchemaRepository _repository;
  private readonly IEtlContext _context;

  public CreateTableSchemaCommandHandler(ITableSchemaRepository repository, IEtlContext context)
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Unit> Handle(
    CreateTableSchemaCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = TableSchemaMapper.ToEntity(command);
    await _repository.AddAsync(entity, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
