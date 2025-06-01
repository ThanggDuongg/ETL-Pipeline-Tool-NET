using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;
using ETLPipelineTool.Application.Features.TableSchemas.Projections;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemasQueryHandler
  : GridQueryHandler<GetTableSchemasQuery, TableSchema, TableSchemaDataDto>
{
  private readonly ITableSchemaRepository _repository;

  public GetTableSchemasQueryHandler(ITableSchemaRepository repository)
  {
    _repository = repository;
  }

  protected override IQueryable<TableSchema> GetBaseQuery(GetTableSchemasQuery request)
  {
    return _repository.GetList();
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
