using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;
using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;
using ETLPipelineTool.Application.Features.TableSchemas.Commands;
using ETLPipelineTool.Application.Features.TableSchemas.Projections;
using ETLPipelineTool.Application.Features.TableSchemas.Queries;
using ETLPipelineTool.Domain.Entities;

namespace ETLPipelineTool.Application.Features.TableSchemas.Mappings;

public static class TableSchemaMapper
{
  public static TableSchema ToEntity(CreateTableSchemaCommand command)
  {
    var dto = command.TableSchema;
    return new TableSchema
    {
      EtlPipelineId = dto.EtlPipelineId,
      TableName = dto.TableName,
      Columns = dto
        .Columns.Select(c => new ColumnSchema
        {
          ColumnName = c.ColumnName,
          DataType = c.DataType,
          IsPrimaryKey = c.IsPrimaryKey,
          IsNullable = c.IsNullable,
          MaxLength = c.MaxLength,
        })
        .ToList(),
      ForeignKeys = dto
        .ForeignKeys.Select(fk => new ForeignKeySchema
        {
          ConstraintName = fk.ConstraintName,
          PrincipalTable = fk.PrincipalTable,
          Columns = fk
            .ForeignKeyColumns.Select(c => new ForeignKeyColumn { ColumnName = c.ColumnName })
            .ToList(),
          PrincipalColumns = fk
            .PrincipalColumns.Select(c => new ForeignKeyPrincipalColumn
            {
              PrincipalColumnName = c.PrincipalColumnName,
            })
            .ToList(),
        })
        .ToList(),
    };
  }

  public static TableSchemaDataDto ToTableSchemaDataDto(TableSchema entity)
  {
    // Use the projection for consistency
    return TableSchemaProjection.AsTableSchemaDataDto().Compile()(entity);
  }

  public static GetTableSchemasQuery ToGetTableSchemasQuery(GridTableSchemasFilterDto dto)
  {
    return new GetTableSchemasQuery(
      dto.GridDataSourceDto.Take,
      dto.GridDataSourceDto.Skip,
      dto.GridDataSourceDto.PreloadAllData,
      dto.GridDataSourceDto.SortFields,
      dto.EtlPipelineId
    );
  }

  public static GetTableSchemaDetailQuery ToGetTableSchemaDetailQuery(Guid id)
  {
    return new GetTableSchemaDetailQuery(id);
  }

  public static DeleteTableSchemaCommand ToDeleteTableSchemaCommand(Guid id)
  {
    return new DeleteTableSchemaCommand(id);
  }

  public static UpdateTableSchemaCommand ToUpdateTableSchemaCommand(UpdateTableSchemaDto dto)
  {
    return new UpdateTableSchemaCommand(dto);
  }
}
