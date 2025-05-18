using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    public class FieldMappingController(IMediator mediator) : BaseApiController
    {
        [HttpPost("grid")]
        public async Task<GridResultDataDto<FieldMappingDataDto>> GetGrid(
            [Required] [FromBody] GridFieldMappingsFilterDto dto,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                FieldMappingMapper.ToGetFieldMappingsQuery(dto),
                cancellationToken
            );
        }

        [HttpGet("{id}")]
        public async Task<FieldMappingDataDto> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                FieldMappingMapper.ToGetFieldMappingDetailQuery(id),
                cancellationToken
            );
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(
                FieldMappingMapper.ToDeleteFieldMappingCommand(id),
                cancellationToken
            );
        }

        [HttpPut]
        public async Task Update(
            [FromBody] UpdateFieldMappingDto dto,
            CancellationToken cancellationToken
        )
        {
            await mediator.Send(
                FieldMappingMapper.ToUpdateFieldMappingCommand(dto),
                cancellationToken
            );
        }

        [HttpPost]
        public async Task<Guid> Create(
            [FromBody] CreateFieldMappingDto dto,
            CancellationToken cancellationToken
        )
        {
            return await mediator.Send(
                FieldMappingMapper.ToCreateFieldMappingCommand(dto),
                cancellationToken
            );
        }
    }
}
