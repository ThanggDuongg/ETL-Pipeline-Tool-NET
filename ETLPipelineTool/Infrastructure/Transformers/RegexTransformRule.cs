using System.Text.RegularExpressions;
using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers
{
  public class RegexTransformRule : ITransformRule
  {
    public Task<object?> ApplyAsync(
      Dictionary<string, object?> columns,
      List<string> sourceFields,
      string? transformConfig,
      CancellationToken cancellationToken = default
    )
    {
      // Get input value (either from first source field or previous result)
      object? inputValue = null;

      if (columns.TryGetValue("result", out var prevResult) && prevResult != null)
      {
        inputValue = prevResult;
      }
      else if (sourceFields.Count > 0 && columns.TryGetValue(sourceFields[0], out var val))
      {
        inputValue = val;
      }

      if (inputValue == null)
      {
        return Task.FromResult<object?>(null);
      }

      string inputString = inputValue.ToString() ?? "";

      // Default values
      string pattern = ".*";
      string replacement = "$0";

      // Parse config
      if (!string.IsNullOrWhiteSpace(transformConfig))
      {
        try
        {
          var configObj = JsonSerializer.Deserialize<Dictionary<string, string>>(transformConfig);

          if (configObj != null)
          {
            if (configObj.TryGetValue("pattern", out var configPattern))
            {
              pattern = configPattern;
            }

            if (configObj.TryGetValue("replacement", out var configReplacement))
            {
              replacement = configReplacement;
            }
          }
        }
        catch
        {
          // Ignore invalid config
        }
      }

      try
      {
        var result = Regex.Replace(inputString, pattern, replacement);
        return Task.FromResult<object?>(result);
      }
      catch (RegexParseException)
      {
        // Return original value if regex is invalid
        return Task.FromResult<object?>(inputString);
      }
    }
  }
}
