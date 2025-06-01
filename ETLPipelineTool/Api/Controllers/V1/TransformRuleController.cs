using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;
using ETLPipelineTool.Application.Features.TransformRules.Mappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class TransformRuleController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<TransformRuleDataDto>> GetGrid(
      [Required] [FromBody] GridTransformRulesFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        TransformRuleMapper.ToGetTransformRulesQuery(dto),
        cancellationToken
      );
    }

    [HttpGet("{id}")]
    public async Task<TransformRuleDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        TransformRuleMapper.ToGetTransformRuleDetailQuery(id),
        cancellationToken
      );
    }

    [HttpPut]
    public async Task Update(
      [FromBody] UpdateTransformRuleDto dto,
      CancellationToken cancellationToken
    )
    {
      await mediator.Send(TransformRuleMapper.ToUpdateTransformRuleCommand(dto), cancellationToken);
    }
  }
}
