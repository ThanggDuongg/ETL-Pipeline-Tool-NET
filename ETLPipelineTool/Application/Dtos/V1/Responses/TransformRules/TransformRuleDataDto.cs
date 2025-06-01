namespace ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules
{
  public record TransformRuleDataDto
  {
    public Guid Id { get; init; }
    public Guid FieldMappingId { get; init; }
    public int Sequence { get; init; }
    public TransformRuleType RuleType { get; init; }
    public string RuleConfigurationJson { get; init; } = default!;
    public byte[]? RowVersion { get; init; }

    public TransformRuleDataDto(
      Guid id,
      Guid fieldMappingId,
      int sequence,
      TransformRuleType ruleType,
      string ruleConfigurationJson,
      byte[]? rowVersion
    )
    {
      Id = id;
      FieldMappingId = fieldMappingId;
      Sequence = sequence;
      RuleType = ruleType;
      RuleConfigurationJson = ruleConfigurationJson;
      RowVersion = rowVersion;
    }

    public TransformRuleDataDto() { }
  }
}
