using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;
using ETLPipelineTool.Application.Features.TransformRules.Commands;
using ETLPipelineTool.Application.Features.TransformRules.Queries;

namespace ETLPipelineTool.Application.Features.TransformRules.Mappings;

public static class TransformRuleMapper
{
  public static GetTransformRulesQuery ToGetTransformRulesQuery(GridTransformRulesFilterDto dto)
  {
    return new GetTransformRulesQuery(
      dto.GridDataSourceDto.Take,
      dto.GridDataSourceDto.Skip,
      dto.GridDataSourceDto.PreloadAllData,
      dto.GridDataSourceDto.SortFields,
      dto.FieldMappingId
    );
  }

  public static GetTransformRuleDetailQuery ToGetTransformRuleDetailQuery(Guid id)
  {
    return new GetTransformRuleDetailQuery(id);
  }

  public static UpdateTransformRuleCommand ToUpdateTransformRuleCommand(UpdateTransformRuleDto dto)
  {
    return new UpdateTransformRuleCommand(
      dto.Id!.Value,
      dto.RuleConfigurationJson,
      dto.RowVersion!
    );
  }
}
