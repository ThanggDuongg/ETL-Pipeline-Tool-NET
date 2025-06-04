using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemaFromSourceCommandHandler(
  IExtractorFactory extractorFactory,
  ITableSchemaRepository tableSchemaRepository,
  IEtlPipelineRepository etlPipelineRepository,
  IEtlContext context
) : IRequestHandler<SyncTableSchemaFromSourceCommand, Unit>
{
  public async Task<Unit> Handle(
    SyncTableSchemaFromSourceCommand command,
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
    var sourceSchema = await schemaExtractor.GetTableSchemaAsync(
      pipeline.SourceConfigurationJson,
      command.TableName,
      cancellationToken
    );

    var existingSchema = await tableSchemaRepository
      .Get()
      .SingleOrDefaultAsync(
        x =>
          x.EtlPipelineId == command.EtlPipelineId
          && x.TableName.Equals(sourceSchema.TableName, StringComparison.OrdinalIgnoreCase),
        cancellationToken
      );

    if (existingSchema != null)
    {
      await tableSchemaRepository.DeleteAsync(existingSchema);
    }

    var tableSchema = new TableSchema
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
          Columns = [.. fk.Columns.Select(c => new ForeignKeyColumn { ColumnName = c.ColumnName })],
          PrincipalColumns =
          [
            .. fk.PrincipalColumns.Select(pc => new ForeignKeyPrincipalColumn
            {
              PrincipalColumnName = pc.PrincipalColumnName,
            }),
          ],
        }),
      ],
    };

    await tableSchemaRepository.AddAsync(tableSchema, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
