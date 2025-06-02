using ETLPipelineTool.Application.Features.TableSchemas.Mappings;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class CreateTableSchemaCommandHandler(ITableSchemaRepository repository, IEtlContext context)
  : IRequestHandler<CreateTableSchemaCommand, Unit>
{
  public async Task<Unit> Handle(
    CreateTableSchemaCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = TableSchemaMapper.ToEntity(command);
    await repository.AddAsync(entity, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
