using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  public class ConcatTransformRule : ITransformRule
  {
    public Task<object?> ApplyAsync(
      Dictionary<string, object?> columns,
      List<string> sourceFields,
      string? transformConfig,
      CancellationToken cancellationToken = default
    )
    {
      string separator = "";

      if (!string.IsNullOrWhiteSpace(transformConfig))
      {
        try
        {
          var configObj = JsonSerializer.Deserialize<Dictionary<string, string>>(transformConfig);
          if (configObj != null && configObj.TryGetValue("separator", out var configSeparator))
          {
            separator = configSeparator;
          }
        }
        catch
        {
          // Ignore invalid config
        }
      }

      var result = string.Join(
        separator,
        sourceFields
          .Select(f => columns.TryGetValue(f, out var v) ? v?.ToString() : "")
          .Where(v => v != null)
      );

      return Task.FromResult<object?>(result);
    }
  }
}
