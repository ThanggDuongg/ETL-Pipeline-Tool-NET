namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class TableSchemaRepository(IEtlContext etlContext)
    : BaseRepository<TableSchema>(etlContext),
      ITableSchemaRepository { }
}
