namespace ETLPipelineTool.Infrastructure.Persistence.Interceptors
{
  public class SlowQueryDetectionInterceptor(ILogger<SlowQueryDetectionInterceptor> logger)
    : DbCommandInterceptor
  {
    private const int slowQueryThresholdInMilliSecond = 5000;

    public override ValueTask<DbDataReader> ReaderExecutedAsync(
      DbCommand command,
      CommandExecutedEventData eventData,
      DbDataReader result,
      CancellationToken cancellationToken = default
    )
    {
      if (eventData.Duration.TotalMilliseconds > slowQueryThresholdInMilliSecond)
      {
        logger.LogWarning(
          "Slow Query Detected. {CommandText}  TotalMilliSeconds: {TotalMilliseconds}",
          command.CommandText,
          eventData.Duration.TotalMilliseconds
        );
      }
      return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    public override DbDataReader ReaderExecuted(
      DbCommand command,
      CommandExecutedEventData eventData,
      DbDataReader result
    )
    {
      if (eventData.Duration.TotalMilliseconds > slowQueryThresholdInMilliSecond)
      {
        logger.LogWarning(
          "Slow Query Detected. {CommandText}  TotalMilliSeconds: {TotalMilliseconds}",
          command.CommandText,
          eventData.Duration.TotalMilliseconds
        );
      }
      return base.ReaderExecuted(command, eventData, result);
    }
  }
}
