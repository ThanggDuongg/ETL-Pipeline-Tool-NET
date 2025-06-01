namespace ETLPipelineTool.Application.Features.TransformRules.Commands;

public class UpdateTransformRuleCommandHandler : IRequestHandler<UpdateTransformRuleCommand, Unit>
{
  private readonly ITransformRuleRepository _repository;
  private readonly IEtlContext _context;

  public UpdateTransformRuleCommandHandler(ITransformRuleRepository repository, IEtlContext context)
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Unit> Handle(
    UpdateTransformRuleCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);

    entity.RuleConfigurationJson = command.RuleConfigurationJson;
    entity.RowVersion = command.RowVersion;

    await _repository.UpdateAsync(entity, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
