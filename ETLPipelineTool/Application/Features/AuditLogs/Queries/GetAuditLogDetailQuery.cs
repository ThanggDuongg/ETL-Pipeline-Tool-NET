using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogDetailQuery(Guid Id) : IRequest<AuditLogDataDto>
{
  public Guid Id { get; } = Id;
}
