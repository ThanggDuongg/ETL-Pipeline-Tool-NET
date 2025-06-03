using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;

namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings
{
  public record CreateFieldMappingDto(
    Guid EtlPipelineId,
    int Order,
    ICollection<CreateFieldMappingSourceDto> FieldMappingSources,
    string TargetField,
    ICollection<CreateTransformRuleDto> TransformRules
  );
}
