using ETLPipelineTool.Application.Dtos.V1.Requests.EtlExecutionLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Mappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class EtlExecutionLogController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<EtlExecutionLogDataDto>> GetGrid(
      [Required] [FromBody] GridEtlExecutionLogsFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        EtlExecutionLogMapper.ToGetEtlExecutionLogsQuery(dto),
        cancellationToken
      );
    }

    [HttpGet("{id}")]
    public async Task<EtlExecutionLogDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        new Application.Features.EtlExecutionLogs.Queries.GetEtlExecutionLogDetailQuery(id),
        cancellationToken
      );
    }
  }
}
