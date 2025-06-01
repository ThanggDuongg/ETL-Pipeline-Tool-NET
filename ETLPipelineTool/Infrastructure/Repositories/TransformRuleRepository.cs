namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class TransformRuleRepository(IEtlContext etlContext)
    : BaseRepository<TransformRule>(etlContext),
      ITransformRuleRepository { }
}
