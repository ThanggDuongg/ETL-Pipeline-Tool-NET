namespace ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;

public record GridTransformRulesFilterDto(
  GridDataSourceDto GridDataSourceDto,
  Guid? FieldMappingId = null
);
