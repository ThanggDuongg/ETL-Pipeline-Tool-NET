using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;
using ETLPipelineTool.Application.Features.TableSchemas.Projections;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemasQueryHandler(ITableSchemaRepository repository)
  : GridQueryHandler<GetTableSchemasQuery, TableSchema, TableSchemaDataDto>
{
  protected override IQueryable<TableSchema> GetBaseQuery(GetTableSchemasQuery request)
  {
    return repository.Get();
  }

  protected override IQueryable<TableSchema> ApplyFiltering(
    IQueryable<TableSchema> query,
    GetTableSchemasQuery request
  )
  {
    if (request.EtlPipelineId.HasValue)
    {
      query = query.Where(x => x.EtlPipelineId == request.EtlPipelineId.Value);
    }

    return query;
  }

  protected override Expression<Func<TableSchema, TableSchemaDataDto>> BuildFullProjection()
  {
    return TableSchemaProjection.AsTableSchemaDataDto();
  }
}
