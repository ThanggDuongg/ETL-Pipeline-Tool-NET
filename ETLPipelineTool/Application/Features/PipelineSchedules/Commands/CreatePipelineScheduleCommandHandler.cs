using ETLPipelineTool.Application.Features.PipelineSchedules.Mappings;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class CreatePipelineScheduleCommandHandler(
  IPipelineScheduleRepository repository,
  IEtlContext context
) : IRequestHandler<CreatePipelineScheduleCommand, Guid>
{
  public async Task<Guid> Handle(
    CreatePipelineScheduleCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = PipelineScheduleMapper.ToEntity(command);

    await repository.AddAsync(entity, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return entity.Id;
  }
}
