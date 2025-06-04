using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Projections;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogsQueryHandler(IEtlExecutionLogRepository repository)
  : GridQueryHandler<GetEtlExecutionLogsQuery, EtlExecutionLog, EtlExecutionLogDataDto>
{
  protected override IQueryable<EtlExecutionLog> GetBaseQuery(GetEtlExecutionLogsQuery request)
  {
    return repository.Get();
  }

  protected override IQueryable<EtlExecutionLog> ApplyFiltering(
    IQueryable<EtlExecutionLog> query,
    GetEtlExecutionLogsQuery request
  )
  {
    if (request.EtlPipelineId.HasValue)
    {
      query = query.Where(x => x.EtlPipelineId == request.EtlPipelineId.Value);
    }

    if (request.Status.HasValue)
    {
      query = query.Where(x => x.Status == request.Status.Value);
    }

    if (request.StartedAtFrom.HasValue)
    {
      query = query.Where(x => x.StartedAt >= request.StartedAtFrom.Value);
    }

    if (request.StartedAtTo.HasValue)
    {
      query = query.Where(x => x.StartedAt <= request.StartedAtTo.Value);
    }

    return query;
  }

  protected override Expression<Func<EtlExecutionLog, EtlExecutionLogDataDto>> BuildFullProjection()
  {
    return EtlExecutionLogProjection.AsEtlExecutionLogDataDto();
  }
}
