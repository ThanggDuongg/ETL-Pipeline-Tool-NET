using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Projections
{
  public static class TableSchemaProjection
  {
    public static Expression<Func<TableSchema, TableSchemaDataDto>> AsTableSchemaDataDto()
    {
      return entity => new TableSchemaDataDto
      {
        Id = entity.Id,
        EtlPipelineId = entity.EtlPipelineId,
        TableName = entity.TableName,
        Columns = entity
          .Columns.Select(c => new ColumnSchemaDataDto
          {
            Id = c.Id,
            ColumnName = c.ColumnName,
            DataType = c.DataType,
            IsPrimaryKey = c.IsPrimaryKey,
            IsNullable = c.IsNullable,
            MaxLength = c.MaxLength,
            RowVersion = c.RowVersion,
          })
          .ToList(),
        ForeignKeys = entity
          .ForeignKeys.Select(fk => new ForeignKeySchemaDataDto
          {
            Id = fk.Id,
            ConstraintName = fk.ConstraintName,
            PrincipalTable = fk.PrincipalTable,
            ForeignKeyColumns = fk
              .Columns.Select(c => new ForeignKeyColumnDataDto
              {
                Id = c.Id,
                ColumnName = c.ColumnName,
              })
              .ToList(),
            PrincipalColumns = fk
              .PrincipalColumns.Select(c => new ForeignKeyPrincipalColumnDataDto
              {
                Id = c.Id,
                PrincipalColumnName = c.PrincipalColumnName,
              })
              .ToList(),
            RowVersion = fk.RowVersion!,
          })
          .ToList(),
        RowVersion = entity.RowVersion,
      };
    }
  }
}
