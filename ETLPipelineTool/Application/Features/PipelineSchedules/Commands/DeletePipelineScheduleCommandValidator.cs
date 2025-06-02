namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class DeletePipelineScheduleCommandValidator
  : AbstractValidator<DeletePipelineScheduleCommand>
{
  public DeletePipelineScheduleCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
