namespace ETLPipelineTool.Application.Features.TransformRules.Commands;

public record UpdateTransformRuleCommand(Guid Id, string RuleConfigurationJson, byte[] RowVersion)
  : IRequest<Unit>;
