using FluentValidation;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries;

public class GetPipelineScheduleDetailQueryValidator
  : AbstractValidator<GetPipelineScheduleDetailQuery>
{
  public GetPipelineScheduleDetailQueryValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
