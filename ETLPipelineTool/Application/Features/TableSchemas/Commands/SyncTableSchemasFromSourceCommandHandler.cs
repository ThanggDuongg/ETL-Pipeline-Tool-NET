using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemasFromSourceCommandHandler(
  IExtractorFactory extractorFactory,
  ITableSchemaRepository tableSchemaRepository,
  IEtlPipelineRepository etlPipelineRepository,
  IEtlContext context
) : IRequestHandler<SyncTableSchemasFromSourceCommand, Unit>
{
  public async Task<Unit> Handle(
    SyncTableSchemasFromSourceCommand command,
    CancellationToken cancellationToken
  )
  {
    var pipeline = await etlPipelineRepository.GetByIdAsync(
      command.EtlPipelineId,
      null,
      (x) => new { x.SourceType, x.SourceConfigurationJson },
      cancellationToken
    );

    if (pipeline.SourceType != PipelineSourceType.MssqlDatabase)
    {
      throw new NotSupportedException(
        $"Schema extraction is only supported for MSSQL databases. Current source type: {pipeline.SourceType}"
      );
    }

    var schemaExtractor = extractorFactory.CreateSchemaExtractor(pipeline.SourceType);
    var tableNames = command.TableNames;

    if (tableNames == null || tableNames.Count == 0)
    {
      tableNames = await schemaExtractor.GetTableNamesAsync(
        pipeline.SourceConfigurationJson,
        cancellationToken
      );
    }

    var existingSchemas = await tableSchemaRepository
      .Get()
      .Where(s => s.EtlPipelineId == command.EtlPipelineId)
      .ToListAsync(cancellationToken);

    var sourceSchemas = await schemaExtractor.GetTableSchemasAsync(
      pipeline.SourceConfigurationJson,
      tableNames,
      cancellationToken
    );

    var schemasToDelete = existingSchemas
      .Where(s =>
        sourceSchemas.Any(ss =>
          ss.TableName.Equals(s.TableName, StringComparison.OrdinalIgnoreCase)
        )
      )
      .ToList();

    foreach (var schema in schemasToDelete)
    {
      await tableSchemaRepository.DeleteAsync(schema);
    }

    var newSchemas = sourceSchemas
      .Select(sourceSchema => new TableSchema
      {
        EtlPipelineId = command.EtlPipelineId,
        TableName = sourceSchema.TableName,
        Columns =
        [
          .. sourceSchema.Columns.Select(c => new ColumnSchema
          {
            ColumnName = c.ColumnName,
            DataType = c.DataType,
            IsPrimaryKey = c.IsPrimaryKey,
            IsNullable = c.IsNullable,
            MaxLength = c.MaxLength,
          }),
        ],
        ForeignKeys =
        [
          .. sourceSchema.ForeignKeys.Select(fk => new ForeignKeySchema
          {
            ConstraintName = fk.ConstraintName,
            PrincipalTable = fk.PrincipalTable,
            Columns =
            [
              .. fk.Columns.Select(c => new ForeignKeyColumn { ColumnName = c.ColumnName }),
            ],
            PrincipalColumns =
            [
              .. fk.PrincipalColumns.Select(pc => new ForeignKeyPrincipalColumn
              {
                PrincipalColumnName = pc.PrincipalColumnName,
              }),
            ],
          }),
        ],
      })
      .ToList();

    if (newSchemas.Count != 0)
    {
      await tableSchemaRepository.AddRangeAsync(newSchemas, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);
    }

    return Unit.Value;
  }
}
