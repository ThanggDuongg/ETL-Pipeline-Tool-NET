using ETLPipelineTool.Domain.Enums;
using ETLPipelineTool.Infrastructure.Transformers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  /// <summary>
  /// Factory for creating transform rule instances
  /// </summary>
  public class TransformRuleFactory : ITransformRuleFactory
  {
    private readonly IServiceProvider _serviceProvider;

    public TransformRuleFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates a transform rule instance based on rule type
    /// </summary>
    public ITransformRule Create(TransformRuleType ruleType)
    {
      return ruleType switch
      {
        TransformRuleType.Identity => _serviceProvider.GetService<IdentityTransformRule>()
          ?? new IdentityTransformRule(),
        TransformRuleType.Concat => _serviceProvider.GetService<ConcatTransformRule>()
          ?? new ConcatTransformRule(),
        TransformRuleType.IfNull => _serviceProvider.GetService<IfNullTransformRule>()
          ?? new IfNullTransformRule(),
        TransformRuleType.Regex => _serviceProvider.GetService<RegexTransformRule>()
          ?? new RegexTransformRule(),
        _ => throw new ArgumentException(
          $"Unsupported transform rule type: {ruleType}",
          nameof(ruleType)
        ),
      };
    }
  }
}
