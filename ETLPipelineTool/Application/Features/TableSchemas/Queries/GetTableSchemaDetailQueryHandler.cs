using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;
using ETLPipelineTool.Application.Features.TableSchemas.Projections;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemaDetailQueryHandler(ITableSchemaRepository repository)
  : IRequestHandler<GetTableSchemaDetailQuery, TableSchemaDataDto>
{
  public async Task<TableSchemaDataDto> Handle(
    GetTableSchemaDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    return await repository.GetByIdAsync(
      request.Id,
      null,
      TableSchemaProjection.AsTableSchemaDataDto(),
      cancellationToken
    );
  }
}
