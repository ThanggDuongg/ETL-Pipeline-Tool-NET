using ETLPipelineTool.Application.Dtos.V1.Requests.AuditLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;
using ETLPipelineTool.Application.Features.AuditLogs.Mappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class AuditLogController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<AuditLogDataDto>> GetGrid(
      [Required] [FromBody] GridAuditLogsFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(AuditLogMapper.ToGetAuditLogsQuery(dto), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<AuditLogDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        new Application.Features.AuditLogs.Queries.GetAuditLogDetailQuery(id),
        cancellationToken
      );
    }
  }
}
