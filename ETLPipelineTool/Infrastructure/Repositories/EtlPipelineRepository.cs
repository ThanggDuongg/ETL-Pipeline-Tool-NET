namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class EtlPipelineRepository(IEtlContext etlContext)
    : BaseRepository<EtlPipeline>(etlContext),
      IEtlPipelineRepository { }
}
