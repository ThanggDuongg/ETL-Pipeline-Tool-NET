namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class DeletePipelineScheduleCommand(Guid Id) : IRequest<Unit>
{
  public Guid Id { get; } = Id;
}
