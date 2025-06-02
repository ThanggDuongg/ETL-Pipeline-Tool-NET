using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;
using ETLPipelineTool.Application.Features.TableSchemas.Commands;
using ETLPipelineTool.Application.Features.TableSchemas.Mappings;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class TableSchemaController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<TableSchemaDataDto>> GetGrid(
      [Required] [FromBody] GridTableSchemasFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(TableSchemaMapper.ToGetTableSchemasQuery(dto), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<TableSchemaDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        TableSchemaMapper.ToGetTableSchemaDetailQuery(id),
        cancellationToken
      );
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
      await mediator.Send(TableSchemaMapper.ToDeleteTableSchemaCommand(id), cancellationToken);
    }

    [HttpPut]
    public async Task Update(
      [FromBody] UpdateTableSchemaDto dto,
      CancellationToken cancellationToken
    )
    {
      await mediator.Send(TableSchemaMapper.ToUpdateTableSchemaCommand(dto), cancellationToken);
    }

    [HttpPost]
    public async Task Create(
      [FromBody] CreateTableSchemaDto dto,
      CancellationToken cancellationToken
    )
    {
      await mediator.Send(new CreateTableSchemaCommand(dto), cancellationToken);
    }
  }
}
