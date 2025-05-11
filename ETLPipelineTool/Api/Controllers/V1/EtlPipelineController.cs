using ETLPipelineTool.Application.Dtos.V1.Requests;
using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    public class EtlPipelineController(IMediator mediator) : BaseApiController
    {
        [HttpPost("grid")]
        public async Task<GridResultDataDto<EtlPipelineDataDto>> GetGrid(
            [Required] [FromBody] GridEtlPipelinesFilterDto dto,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                EtlPipelineMapper.ToGetEtlPipelinesQuery(dto),
                cancellationToken
            );
        }

        [HttpGet("{id}")]
        public async Task<EtlPipelineDataDto> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                EtlPipelineMapper.ToGetEtlPipelineDetailQuery(id),
                cancellationToken
            );
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(
                EtlPipelineMapper.ToDeleteEtlPipelineCommand(id),
                cancellationToken
            );
        }

        [HttpPut]
        public async Task Update(
            [FromBody] UpdateEtlPipelineDto dto,
            CancellationToken cancellationToken
        )
        {
            await mediator.Send(
                EtlPipelineMapper.ToUpdateEtlPipelineCommand(dto),
                cancellationToken
            );
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<Guid> Create(
            [FromBody] CreateEtlPipelineDto dto,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                EtlPipelineMapper.ToCreateEtlPipelineCommand(dto),
                cancellationToken
            );
        }
    }
}
