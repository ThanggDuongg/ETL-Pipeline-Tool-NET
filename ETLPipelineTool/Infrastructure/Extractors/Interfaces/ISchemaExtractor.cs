namespace ETLPipelineTool.Infrastructure.Extractors.Interfaces
{
  public interface ISchemaExtractor
  {
    Task<TableSchema> GetTableSchemaAsync(
      string configurationJson,
      string tableName,
      CancellationToken cancellationToken = default
    );

    Task<ICollection<string>> GetTableNamesAsync(
      string configurationJson,
      CancellationToken cancellationToken = default
    );

    Task<ICollection<TableSchema>> GetTableSchemasAsync(
      string configurationJson,
      ICollection<string> tableNames,
      CancellationToken cancellationToken = default
    );
  }
}
