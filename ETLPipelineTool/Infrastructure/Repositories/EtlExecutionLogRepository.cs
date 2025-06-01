namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class EtlExecutionLogRepository(IEtlContext etlContext)
    : BaseRepository<EtlExecutionLog>(etlContext),
      IEtlExecutionLogRepository { }
}
