using FluentValidation;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogsQueryValidator : AbstractValidator<GetAuditLogsQuery>
{
  public GetAuditLogsQueryValidator()
  {
    When(
      x => x.CreatedOnFrom.HasValue && x.CreatedOnTo.HasValue,
      () =>
      {
        RuleFor(x => x.CreatedOnTo)
          .GreaterThanOrEqualTo(x => x.CreatedOnFrom)
          .WithMessage("End date must be greater than or equal to start date");
      }
    );
  }
}
