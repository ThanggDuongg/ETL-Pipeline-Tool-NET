namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class FieldMappingRepository(IEtlContext etlContext)
    : BaseRepository<FieldMapping>(etlContext),
      IFieldMappingRepository { }
}
