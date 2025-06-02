namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogsQueryValidator : AbstractValidator<GetAuditLogsQuery>
{
  public GetAuditLogsQueryValidator()
  {
    When(
      x => x.CreatedOnFrom.HasValue && x.CreatedOnTo.HasValue,
      () =>
      {
        RuleFor(x => x.CreatedOnTo).GreaterThanOrEqualTo(x => x.CreatedOnFrom);
      }
    );
  }
}
