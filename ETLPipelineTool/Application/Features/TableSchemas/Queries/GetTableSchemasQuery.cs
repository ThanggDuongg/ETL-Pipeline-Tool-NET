using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemasQuery(
  int take,
  int skip,
  bool preloadAllData,
  ICollection<SortField> sortFields,
  Guid? etlPipelineId = null
) : GridQuery<GridResultDataDto<TableSchemaDataDto>>(take, skip, preloadAllData, sortFields)
{
  public Guid? EtlPipelineId { get; } = etlPipelineId;
}
