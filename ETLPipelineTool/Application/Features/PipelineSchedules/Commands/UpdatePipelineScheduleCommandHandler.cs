using ETLPipelineTool.Application.Features.PipelineSchedules.Mappings;

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
    PipelineScheduleMapper.UpdateEntity(entity, command);

    await repository.UpdateAsync(entity, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
