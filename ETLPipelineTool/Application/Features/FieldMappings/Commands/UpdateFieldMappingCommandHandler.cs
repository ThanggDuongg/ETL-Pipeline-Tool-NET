namespace ETLPipelineTool.Application.Features.FieldMappings.Commands;

public class UpdateFieldMappingCommandHandler(
    IFieldMappingRepository repository,
    IEtlContext context
) : IRequestHandler<UpdateFieldMappingCommand, Unit>
{
    public async Task<Unit> Handle(
        UpdateFieldMappingCommand command,
        CancellationToken cancellationToken
    )
    {
        var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

        entity.Order = command.Order;
        entity.SourceField = command.SourceField;
        entity.TargetField = command.TargetField;
        entity.TransformExpression = command.TransformExpression;
        entity.RowVersion = command.RowVersion;

        await repository.UpdateAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
