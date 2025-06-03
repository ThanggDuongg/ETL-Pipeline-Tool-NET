namespace ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules
{
  public record CreateTransformRuleDto(
    int Sequence,
    TransformRuleType RuleType,
    string RuleConfigurationJson
  );
}
