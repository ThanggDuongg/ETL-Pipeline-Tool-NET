namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings
{
  public record CreateTransformRuleDto(
    int Sequence,
    TransformRuleType RuleType,
    string RuleConfigurationJson
  );
}
