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
          .OrderBy(x => x.Order)
          .Select(x => new CreateFieldMappingSourceDto(order++, x.SourceField)),
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
          .OrderBy(x => x.Order)
          .Select(x => new UpdateFieldMappingSourceDto(x.Id, order++, x.SourceField, x.RowVersion)),
      ];
    }

    public static void NormalizeSourceFieldOrders(ICollection<FieldMappingSource> sources)
    {
      if (sources == null || sources.Count == 0)
      {
        return;
      }

      int order = 0;
      foreach (var source in sources.OrderBy(x => x.Order))
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
          .OrderBy(x => x.Sequence)
          .Select(x => new CreateTransformRuleDto(sequence++, x.RuleType, x.RuleConfigurationJson)),
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
          .OrderBy(x => x.Sequence)
          .Select(x => new UpdateTransformRuleDto(
            x.Id,
            sequence++,
            x.RuleType,
            x.RuleConfigurationJson,
            x.RowVersion
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
      foreach (var rule in rules.OrderBy(x => x.Sequence))
      {
        rule.Sequence = sequence++;
      }
    }
  }
}
