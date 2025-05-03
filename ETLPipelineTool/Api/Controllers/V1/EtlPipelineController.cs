using ETLPipelineTool.Application.Dtos.V1.Requests;

namespace ETLPipelineTool.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    public class EtlPipelineController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateEtlPipelineDto dto)
        {
            return await _mediator.Send(EtlPipelineMapper.ToCommand(dto));
        }
    }
}
