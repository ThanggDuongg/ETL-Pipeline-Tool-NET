namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class PipelineScheduleRepository(IEtlContext etlContext)
    : BaseRepository<PipelineSchedule>(etlContext),
      IPipelineScheduleRepository { }
}
