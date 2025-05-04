using ETLPipelineTool.Domain.Interfaces.Repositories;

namespace ETLPipelineTool.Infrastructure.Repositories
{
    public class EtlPipelineRepository(IEtlContext etlContext)
        : BaseRepository<EtlPipeline>(etlContext),
            IEtlPipelineRepository { }
}
