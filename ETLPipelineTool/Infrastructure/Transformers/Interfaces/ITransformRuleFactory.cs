namespace ETLPipelineTool.Infrastructure.Transformers.Interfaces
{
  public interface ITransformRuleFactory
  {
    ITransformRule Create(TransformRuleType ruleType);
  }
}
