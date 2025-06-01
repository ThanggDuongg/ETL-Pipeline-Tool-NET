using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Projections;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogDetailQueryHandler
  : IRequestHandler<GetEtlExecutionLogDetailQuery, EtlExecutionLogDataDto>
{
  private readonly IEtlExecutionLogRepository _repository;

  public GetEtlExecutionLogDetailQueryHandler(IEtlExecutionLogRepository repository)
  {
    _repository = repository;
  }

  public async Task<EtlExecutionLogDataDto> Handle(
    GetEtlExecutionLogDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    return await _repository.GetByIdAsync(
      request.Id,
      null,
      EtlExecutionLogProjection.AsEtlExecutionLogDataDto(),
      cancellationToken
    );
  }
}
