using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;
using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;

namespace ETLPipelineTool.Application.Services
{
  public static class FieldMappingOrderService
  {
    public static ICollection<CreateFieldMappingSourceDto> NormalizeSourceFieldOrders(
      ICollection<CreateFieldMappingSourceDto> sources
    )
    {
      if (sources == null || sources.Count == 0)
      {
        return sources ?? [];
      }

      int order = 0;
      return
      [
        .. sources
          .OrderBy(s => s.Order)
          .Select(source => new CreateFieldMappingSourceDto(order++, source.SourceField)),
      ];
    }

    public static ICollection<UpdateFieldMappingSourceDto> NormalizeSourceFieldOrders(
      ICollection<UpdateFieldMappingSourceDto> sources
    )
    {
      if (sources == null || sources.Count == 0)
      {
        return sources ?? [];
      }

      int order = 0;
      return
      [
        .. sources
          .OrderBy(s => s.Order)
          .Select(source => new UpdateFieldMappingSourceDto(
            source.Id,
            order++,
            source.SourceField,
            source.RowVersion
          )),
      ];
    }

    public static void NormalizeSourceFieldOrders(ICollection<FieldMappingSource> sources)
    {
      if (sources == null || sources.Count == 0)
      {
        return;
      }

      int order = 0;
      foreach (FieldMappingSource? source in sources.OrderBy(s => s.Order))
      {
        source.Order = order++;
      }
    }

    public static ICollection<CreateTransformRuleDto> NormalizeTransformRuleSequences(
      ICollection<CreateTransformRuleDto> rules
    )
    {
      if (rules == null || rules.Count == 0)
      {
        return rules ?? [];
      }

      int sequence = 0;
      return
      [
        .. rules
          .OrderBy(r => r.Sequence)
          .Select(rule => new CreateTransformRuleDto(
            sequence++,
            rule.RuleType,
            rule.RuleConfigurationJson
          )),
      ];
    }

    public static ICollection<UpdateTransformRuleDto> NormalizeTransformRuleSequences(
      ICollection<UpdateTransformRuleDto> rules
    )
    {
      if (rules == null || rules.Count == 0)
      {
        return rules ?? [];
      }

      int sequence = 0;
      return
      [
        .. rules
          .OrderBy(r => r.Sequence)
          .Select(rule => new UpdateTransformRuleDto(
            rule.Id,
            sequence++,
            rule.RuleType,
            rule.RuleConfigurationJson,
            rule.RowVersion
          )),
      ];
    }

    public static void NormalizeTransformRuleSequences(ICollection<TransformRule> rules)
    {
      if (rules == null || rules.Count == 0)
      {
        return;
      }

      int sequence = 0;
      foreach (TransformRule? rule in rules.OrderBy(r => r.Sequence))
      {
        rule.Sequence = sequence++;
      }
    }
  }
}
