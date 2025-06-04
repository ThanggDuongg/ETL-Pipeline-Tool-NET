using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  // Transform rule that replaces null values with a default value
  public class IfNullTransformRule : ITransformRule
  {
    public Task<object?> ApplyAsync(
      Dictionary<string, object?> columns,
      List<string> sourceFields,
      string? transformConfig,
      CancellationToken cancellationToken = default
    )
    {
      object? inputValue = null;

      if (columns.TryGetValue("result", out var prevResult))
      {
        inputValue = prevResult;
      }
      else if (sourceFields.Count > 0 && columns.TryGetValue(sourceFields[0], out var val))
      {
        inputValue = val;
      }

      // If input is not null, return it as is
      if (inputValue != null && inputValue != DBNull.Value)
      {
        return Task.FromResult<object?>(inputValue);
      }

      string? defaultValue = null;
      if (!string.IsNullOrWhiteSpace(transformConfig))
      {
        try
        {
          var configObj = JsonSerializer.Deserialize<Dictionary<string, object>>(transformConfig);
          if (configObj != null && configObj.TryGetValue("defaultValue", out var configDefault))
          {
            defaultValue = configDefault?.ToString();
          }
        }
        catch
        {
          // Ignore invalid config
        }
      }

      return Task.FromResult<object?>(defaultValue);
    }
  }
}
