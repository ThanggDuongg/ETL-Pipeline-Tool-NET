namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogDetailQueryValidator : AbstractValidator<GetAuditLogDetailQuery>
{
  public GetAuditLogDetailQueryValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
