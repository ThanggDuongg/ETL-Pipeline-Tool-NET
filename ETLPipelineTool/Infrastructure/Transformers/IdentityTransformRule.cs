using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  public class IdentityTransformRule : ITransformRule
  {
    public Task<object?> ApplyAsync(
      Dictionary<string, object?> columns,
      List<string> sourceFields,
      string? transformConfig,
      CancellationToken cancellationToken = default
    )
    {
      if (
        sourceFields.Count > 0
        && columns.TryGetValue(sourceFields[0], out var val)
        && val is not null
      )
      {
        return Task.FromResult<object?>(val);
      }

      if (!string.IsNullOrWhiteSpace(transformConfig))
      {
        try
        {
          var configObj = JsonSerializer.Deserialize<Dictionary<string, object>>(transformConfig);
          if (configObj != null && configObj.TryGetValue("defaultValue", out var defaultValue))
          {
            return Task.FromResult<object?>(defaultValue);
          }
        }
        catch
        {
          // ignore invalid config
        }
      }

      return Task.FromResult<object?>(null);
    }
  }
}
