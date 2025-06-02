namespace ETLPipelineTool.Application.Features.TransformRules.Commands;

public class UpdateTransformRuleCommand(Guid Id, string RuleConfigurationJson, byte[] RowVersion)
  : IRequest<Unit>
{
  public Guid Id { get; } = Id;
  public string RuleConfigurationJson { get; } = RuleConfigurationJson;
  public byte[] RowVersion { get; } = RowVersion;
}
