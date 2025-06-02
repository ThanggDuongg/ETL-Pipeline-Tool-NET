namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class UpdatePipelineScheduleCommandHandler(
  IPipelineScheduleRepository repository,
  IEtlContext context
) : IRequestHandler<UpdatePipelineScheduleCommand, Unit>
{
  public async Task<Unit> Handle(
    UpdatePipelineScheduleCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = await repository.GetByIdAsync(command.Id, cancellationToken);
    entity.CronExpression = command.CronExpression;
    entity.IsEnabled = command.IsActive;
    entity.StartDate = command.StartDate;
    entity.EndDate = command.EndDate;
    entity.RowVersion = command.RowVersion;

    await repository.UpdateAsync(entity, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
