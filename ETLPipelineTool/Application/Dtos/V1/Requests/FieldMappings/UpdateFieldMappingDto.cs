using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;

namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

public record UpdateFieldMappingDto(
  Guid Id,
  Guid EtlPipelineId,
  int Order,
  ICollection<UpdateFieldMappingSourceDto> FieldMappingSources,
  string TargetField,
  ICollection<UpdateTransformRuleDto> TransformRules,
  byte[] RowVersion
);
