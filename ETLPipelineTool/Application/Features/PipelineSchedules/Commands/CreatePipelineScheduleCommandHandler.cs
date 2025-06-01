using ETLPipelineTool.Application.Features.PipelineSchedules.Mappings;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class CreatePipelineScheduleCommandHandler
  : IRequestHandler<CreatePipelineScheduleCommand, Guid>
{
  private readonly IPipelineScheduleRepository _repository;
  private readonly IEtlContext _context;

  public CreatePipelineScheduleCommandHandler(
    IPipelineScheduleRepository repository,
    IEtlContext context
  )
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Guid> Handle(
    CreatePipelineScheduleCommand command,
    CancellationToken cancellationToken
  )
  {
    var entity = PipelineScheduleMapper.ToEntity(command);

    await _repository.AddAsync(entity, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);

    return entity.Id;
  }
}
