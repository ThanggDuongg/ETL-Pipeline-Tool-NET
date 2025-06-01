using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public record GetEtlExecutionLogDetailQuery(Guid Id) : IRequest<EtlExecutionLogDataDto>;
