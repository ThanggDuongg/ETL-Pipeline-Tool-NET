using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public record GetAuditLogDetailQuery(Guid Id) : IRequest<AuditLogDataDto>;
