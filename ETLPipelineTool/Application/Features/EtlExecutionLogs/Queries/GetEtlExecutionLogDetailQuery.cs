using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogDetailQuery(Guid Id) : IRequest<EtlExecutionLogDataDto>
{
  public Guid Id { get; } = Id;
}
