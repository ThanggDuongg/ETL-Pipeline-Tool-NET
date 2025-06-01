using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRulesQuery(
  int take,
  int skip,
  bool preloadAllData,
  ICollection<SortField> sortFields,
  Guid? fieldMappingId = null
) : GridQuery<GridResultDataDto<TransformRuleDataDto>>(take, skip, preloadAllData, sortFields)
{
  public Guid? FieldMappingId { get; } = fieldMappingId;
}
