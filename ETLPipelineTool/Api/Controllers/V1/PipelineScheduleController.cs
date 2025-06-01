using ETLPipelineTool.Application.Dtos.V1.Requests.PipelineSchedules;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using ETLPipelineTool.Application.Features.PipelineSchedules.Mappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class PipelineScheduleController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<PipelineScheduleDataDto>> GetGrid(
      [Required] [FromBody] GridPipelineSchedulesFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        PipelineScheduleMapper.ToGetPipelineSchedulesQuery(dto),
        cancellationToken
      );
    }

    [HttpGet("{id}")]
    public async Task<PipelineScheduleDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        PipelineScheduleMapper.ToGetPipelineScheduleDetailQuery(id),
        cancellationToken
      );
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
      await mediator.Send(
        PipelineScheduleMapper.ToDeletePipelineScheduleCommand(id),
        cancellationToken
      );
    }

    [HttpPut]
    public async Task Update(
      [FromBody] UpdatePipelineScheduleDto dto,
      CancellationToken cancellationToken
    )
    {
      await mediator.Send(
        PipelineScheduleMapper.ToUpdatePipelineScheduleCommand(dto),
        cancellationToken
      );
    }

    [HttpPost]
    public async Task<Guid> Create(
      [FromBody] CreatePipelineScheduleDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        PipelineScheduleMapper.ToCreatePipelineScheduleCommand(dto),
        cancellationToken
      );
    }
  }
}
