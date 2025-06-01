namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class DeletePipelineScheduleCommandHandler(
  IPipelineScheduleRepository repository,
  IEtlContext context
) : IRequestHandler<DeletePipelineScheduleCommand, Unit>
{
  public async Task<Unit> Handle(
    DeletePipelineScheduleCommand command,
    CancellationToken cancellationToken
  )
  {
    await repository.DeleteByIdAsync(command.Id, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
