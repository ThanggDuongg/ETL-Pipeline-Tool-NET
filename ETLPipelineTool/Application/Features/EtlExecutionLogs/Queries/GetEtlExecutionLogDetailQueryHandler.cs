using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Projections;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogDetailQueryHandler(IEtlExecutionLogRepository repository)
  : IRequestHandler<GetEtlExecutionLogDetailQuery, EtlExecutionLogDataDto>
{
  public async Task<EtlExecutionLogDataDto> Handle(
    GetEtlExecutionLogDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    return await repository.GetByIdAsync(
      request.Id,
      null,
      EtlExecutionLogProjection.AsEtlExecutionLogDataDto(),
      cancellationToken
    );
  }
}
