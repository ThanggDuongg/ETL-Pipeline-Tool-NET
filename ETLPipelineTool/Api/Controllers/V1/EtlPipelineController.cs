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

        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateEtlPipelineDto dto)
        {
            return await mediator.Send(EtlPipelineMapper.ToCreateEtlPipelineCommand(dto));
        }
    }
}
