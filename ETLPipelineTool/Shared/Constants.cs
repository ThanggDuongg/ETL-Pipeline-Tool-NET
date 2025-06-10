namespace ETLPipelineTool.Shared
{
  public static class Constants
  {
    public struct Auth
    {
      public const string ANTIFORGERY_TOKEN_HEADER = "X-XSRF-TOKEN";
    }

    public struct MessageTemplate
    {
      public const string NOT_FOUND = "Entity type={0}, id={1} is not found";
    }

    public readonly struct JsonSetting
    {
      public static readonly JsonSerializerOptions DefaultOptions = new()
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      };
    }
  }
}
