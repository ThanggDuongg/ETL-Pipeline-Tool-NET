using System.Collections.Concurrent;
using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  public class Transformer : ITransformer
  {
    private readonly ILogger<Transformer> _logger;
    private readonly ITransformRuleFactory _transformRuleFactory;
    private readonly ConcurrentDictionary<TransformRuleType, ITransformRule> _ruleCache;

    public Transformer(ILogger<Transformer> logger, ITransformRuleFactory transformRuleFactory)
    {
      _logger = logger;
      _transformRuleFactory = transformRuleFactory;
      _ruleCache = new ConcurrentDictionary<TransformRuleType, ITransformRule>();
    }

    public async Task<IEnumerable<IDictionary<string, object>>> TransformAsync(
      EtlPipeline etlPipeline,
      IEnumerable<IDictionary<string, object>> rawData,
      CancellationToken cancellationToken = default
    )
    {
      if (rawData == null || !rawData.Any())
      {
        _logger.LogWarning("No data to transform for pipeline {PipelineId}", etlPipeline.Id);
        return [];
      }

      var fieldMappings = etlPipeline.FieldMappings.ToList();

      if (fieldMappings.Count == 0)
      {
        _logger.LogWarning("No field mappings defined for pipeline {PipelineId}", etlPipeline.Id);
        return rawData;
      }

      var result = await TransformDataAsync(rawData, fieldMappings, cancellationToken);

      return result;
    }

    private async Task<List<IDictionary<string, object>>> TransformDataAsync(
      IEnumerable<IDictionary<string, object>> rawData,
      List<FieldMapping> fieldMappings,
      CancellationToken cancellationToken
    )
    {
      var dataCount = rawData.Count();

      // For small datasets, process sequentially
      if (dataCount < 100)
      {
        return await TransformDataSequentiallyAsync(rawData, fieldMappings, cancellationToken);
      }

      // For larger datasets, use parallel processing
      return await TransformDataInParallelAsync(rawData, fieldMappings, cancellationToken);
    }

    private async Task<List<IDictionary<string, object>>> TransformDataSequentiallyAsync(
      IEnumerable<IDictionary<string, object>> rawData,
      List<FieldMapping> fieldMappings,
      CancellationToken cancellationToken
    )
    {
      var result = new List<IDictionary<string, object>>(rawData.Count());

      foreach (var row in rawData)
      {
        cancellationToken.ThrowIfCancellationRequested();
        var transformedRow = await TransformRowAsync(row, fieldMappings, cancellationToken);
        result.Add(transformedRow);
      }

      return result;
    }

    private async Task<List<IDictionary<string, object>>> TransformDataInParallelAsync(
      IEnumerable<IDictionary<string, object>> rawData,
      List<FieldMapping> fieldMappings,
      CancellationToken cancellationToken
    )
    {
      var result = new ConcurrentBag<IDictionary<string, object>>();
      var options = new ParallelOptions
      {
        MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount),
        CancellationToken = cancellationToken,
      };

      await Parallel.ForEachAsync(
        rawData,
        options,
        async (row, token) =>
        {
          var transformedRow = await TransformRowAsync(row, fieldMappings, token);
          result.Add(transformedRow);
        }
      );

      return [.. result];
    }

    private async Task<IDictionary<string, object>> TransformRowAsync(
      IDictionary<string, object> row,
      List<FieldMapping> fieldMappings,
      CancellationToken cancellationToken
    )
    {
      var result = new Dictionary<string, object>(row);

      foreach (var mapping in fieldMappings)
      {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
          var value = await ApplyFieldMappingAsync(row, mapping, cancellationToken);
          result[mapping.TargetField] = value ?? DBNull.Value;
        }
        catch (Exception ex)
        {
          _logger.LogError(
            ex,
            "Error applying field mapping {MappingId} for target field {TargetField}",
            mapping.Id,
            mapping.TargetField
          );

          result[mapping.TargetField] = DBNull.Value;
        }
      }

      return result;
    }

    private async Task<object?> ApplyFieldMappingAsync(
      IDictionary<string, object> row,
      FieldMapping mapping,
      CancellationToken cancellationToken
    )
    {
      var sourceFields = mapping
        .SourceFields.OrderBy(s => s.Order)
        .Select(s => s.SourceField)
        .ToList();

      var sourceValues = new Dictionary<string, object?>();
      foreach (var field in sourceFields)
      {
        sourceValues[field] = row.TryGetValue(field, out var value) ? value : null;
      }

      if (mapping.TransformRules.Count == 0)
      {
        return sourceFields.Count > 0 && row.TryGetValue(sourceFields[0], out var value)
          ? value
          : null;
      }

      return await ApplyTransformRulesAsync(
        sourceValues,
        sourceFields,
        mapping.TransformRules.OrderBy(r => r.Sequence).ToList(),
        cancellationToken
      );
    }

    private async Task<object?> ApplyTransformRulesAsync(
      Dictionary<string, object?> sourceValues,
      List<string> sourceFields,
      List<TransformRule> rules,
      CancellationToken cancellationToken
    )
    {
      object? result = null;
      var intermediateValues = new Dictionary<string, object?>(sourceValues);

      foreach (var rule in rules)
      {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
          var transformerRule = GetOrCreateTransformRule(rule.RuleType);

          result = await transformerRule.ApplyAsync(
            intermediateValues,
            sourceFields,
            rule.RuleConfigurationJson,
            cancellationToken
          );

          // Store result for next rule
          intermediateValues["result"] = result;
        }
        catch (Exception ex)
        {
          _logger.LogError(
            ex,
            "Error applying transform rule {RuleId} of type {RuleType}",
            rule.Id,
            rule.RuleType
          );
        }
      }

      return result;
    }

    private ITransformRule GetOrCreateTransformRule(TransformRuleType ruleType)
    {
      return _ruleCache.GetOrAdd(ruleType, type => _transformRuleFactory.Create(type));
    }
  }
}
