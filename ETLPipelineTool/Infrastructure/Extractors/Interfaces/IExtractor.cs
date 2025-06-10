using ETLPipelineTool.Domain.ValueObjects;

namespace ETLPipelineTool.Infrastructure.Extractors.Interfaces
{
  public interface IExtractor
  {
    IAsyncEnumerable<TableData> ExtractAsync(
      EtlPipeline etlPipeline,
      CancellationToken cancellationToken = default
    );

    void SetConnection(DbConnection connection);
  }
}
