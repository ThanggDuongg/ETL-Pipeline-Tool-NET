namespace ETLPipelineTool.Application.Features.TransformRules.Commands;

public class UpdateTransformRuleCommandHandler(
  ITransformRuleRepository repository,
  IEtlContext context
) : IRequestHandler<UpdateTransformRuleCommand, Unit>
{
  public async Task<Unit> Handle(
    UpdateTransformRuleCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

    entity.RuleConfigurationJson = command.RuleConfigurationJson;
    entity.RowVersion = command.RowVersion;

    await repository.UpdateAsync(entity, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
