using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings
{
  public record FieldMappingDataDto
  {
    public Guid Id { get; init; }
    public Guid EtlPipelineId { get; init; }
    public int Order { get; init; }
    public ICollection<FieldMappingSourceDataDto> FieldMappingSources { get; init; } = [];
    public string TargetField { get; init; } = default!;
    public ICollection<TransformRuleDataDto> TransformRules { get; init; } = [];
    public byte[]? RowVersion { get; init; }

    public FieldMappingDataDto() { }

    public FieldMappingDataDto(
      Guid id,
      Guid etlPipelineId,
      int order,
      ICollection<FieldMappingSourceDataDto> fieldMappingSources,
      string targetField,
      ICollection<TransformRuleDataDto> transformRules,
      byte[]? rowVersion
    )
    {
      Id = id;
      EtlPipelineId = etlPipelineId;
      Order = order;
      FieldMappingSources = fieldMappingSources;
      TargetField = targetField;
      TransformRules = transformRules;
      RowVersion = rowVersion;
    }
  }
}
