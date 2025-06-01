using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;
using ETLPipelineTool.Application.Features.AuditLogs.Mappings;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogDetailQueryHandler
  : IRequestHandler<GetAuditLogDetailQuery, AuditLogDataDto>
{
  private readonly IAuditLogRepository _repository;

  public GetAuditLogDetailQueryHandler(IAuditLogRepository repository)
  {
    _repository = repository;
  }

  public async Task<AuditLogDataDto> Handle(
    GetAuditLogDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
    return AuditLogMapper.ToAuditLogDataDto(entity);
  }
}
