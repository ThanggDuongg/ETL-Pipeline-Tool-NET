using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;
using ETLPipelineTool.Application.Features.AuditLogs.Projections;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogDetailQueryHandler(IAuditLogRepository repository)
  : IRequestHandler<GetAuditLogDetailQuery, AuditLogDataDto>
{
  public async Task<AuditLogDataDto> Handle(
    GetAuditLogDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    return await repository.GetByIdAsync(
      request.Id,
      null,
      AuditLogProjection.AsAuditLogDataDto(),
      cancellationToken
    );
  }
}
