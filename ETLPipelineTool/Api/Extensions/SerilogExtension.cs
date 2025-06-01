namespace ETLPipelineTool.Api.Extensions
{
  public static class SerilogExtension
  {
    public static ILoggingBuilder AddSerilogService(this ILoggingBuilder loggingBuilder)
    {
      loggingBuilder.ClearProviders();
      loggingBuilder.AddSerilog();
      return loggingBuilder;
    }
  }
}
