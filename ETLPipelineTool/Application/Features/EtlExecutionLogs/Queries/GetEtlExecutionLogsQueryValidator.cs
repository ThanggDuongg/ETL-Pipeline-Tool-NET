using FluentValidation;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogsQueryValidator : AbstractValidator<GetEtlExecutionLogsQuery>
{
  public GetEtlExecutionLogsQueryValidator()
  {
    When(
      x => x.StartedAtFrom.HasValue && x.StartedAtTo.HasValue,
      () =>
      {
        RuleFor(x => x.StartedAtTo)
          .GreaterThanOrEqualTo(x => x.StartedAtFrom)
          .WithMessage("End date must be greater than or equal to start date");
      }
    );
  }
}
