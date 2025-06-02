namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogDetailQueryValidator
  : AbstractValidator<GetEtlExecutionLogDetailQuery>
{
  public GetEtlExecutionLogDetailQueryValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
