using ETLPipelineTool.Application.Dtos.V1.Requests;
using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    public class EtlPipelineController(IMediator mediator) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<EtlPipelineDataDto> GetById([FromRoute] Guid id)
        {
            return await mediator.Send(EtlPipelineMapper.ToGetEtlPipelineDetailQuery(id));
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(EtlPipelineMapper.ToDeleteEtlPipelineCommand(id));
        }

        [HttpPut]
        public async Task Update([FromBody] UpdateEtlPipelineDto dto)
        {
            await mediator.Send(EtlPipelineMapper.ToUpdateEtlPipelineCommand(dto));
        }

        [HttpPost]
        public async Task Create([FromBody] CreateEtlPipelineDto dto)
        {
            await mediator.Send(EtlPipelineMapper.ToCreateEtlPipelineCommand(dto));
        }
    }
}
