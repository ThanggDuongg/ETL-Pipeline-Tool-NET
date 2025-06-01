namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class UpdatePipelineScheduleCommandValidator
  : AbstractValidator<UpdatePipelineScheduleCommand>
{
  public UpdatePipelineScheduleCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.EtlPipelineId).NotEmpty();
    RuleFor(x => x.CronExpression).NotEmpty().Must(BeValidCronExpression);
    RuleFor(x => x.RowVersion).NotNull();

    When(
      x => x.StartDate.HasValue && x.EndDate.HasValue,
      () =>
      {
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
      }
    );
  }

  private static bool BeValidCronExpression(string cronExpression)
  {
    try
    {
      var parts = cronExpression.Split(' ');
      return parts.Length >= 5;
    }
    catch
    {
      return false;
    }
  }
}
