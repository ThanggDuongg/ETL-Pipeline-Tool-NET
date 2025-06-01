using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class UpdateTableSchemaCommandHandler : IRequestHandler<UpdateTableSchemaCommand, Unit>
{
  private readonly ITableSchemaRepository _repository;
  private readonly IEtlContext _context;

  public UpdateTableSchemaCommandHandler(ITableSchemaRepository repository, IEtlContext context)
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Unit> Handle(
    UpdateTableSchemaCommand command,
    CancellationToken cancellationToken
  )
  {
    var dto = command.TableSchema;
    var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken);

    entity.TableName = dto.TableName;
    entity.RowVersion = dto.RowVersion;

    UpdateColumns(entity, dto.Columns);
    UpdateForeignKeys(entity, dto.ForeignKeys);

    await _context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }

  private static void UpdateColumns(TableSchema entity, ICollection<UpdateColumnSchemaDto> columns)
  {
    var columnsToRemove = entity.Columns.Where(c => !columns.Any(dto => dto.Id == c.Id)).ToList();

    foreach (var column in columnsToRemove)
    {
      entity.Columns.Remove(column);
    }

    foreach (var columnDto in columns)
    {
      if (columnDto.Id.HasValue)
      {
        var existingColumn = entity.Columns.FirstOrDefault(c => c.Id == columnDto.Id.Value);
        if (existingColumn != null)
        {
          existingColumn.ColumnName = columnDto.ColumnName;
          existingColumn.DataType = columnDto.DataType;
          existingColumn.IsPrimaryKey = columnDto.IsPrimaryKey;
          existingColumn.IsNullable = columnDto.IsNullable;
          existingColumn.MaxLength = columnDto.MaxLength;

          if (columnDto.RowVersion != null)
          {
            existingColumn.RowVersion = columnDto.RowVersion;
          }
        }
      }
      else
      {
        entity.Columns.Add(
          new ColumnSchema
          {
            ColumnName = columnDto.ColumnName,
            DataType = columnDto.DataType,
            IsPrimaryKey = columnDto.IsPrimaryKey,
            IsNullable = columnDto.IsNullable,
            MaxLength = columnDto.MaxLength,
          }
        );
      }
    }
  }

  private static void UpdateForeignKeys(
    TableSchema entity,
    ICollection<UpdateForeignKeySchemaDto> foreignKeys
  )
  {
    var foreignKeysToRemove = entity
      .ForeignKeys.Where(fk => !foreignKeys.Any(dto => dto.Id == fk.Id))
      .ToList();

    foreach (var foreignKey in foreignKeysToRemove)
    {
      entity.ForeignKeys.Remove(foreignKey);
    }

    foreach (var foreignKeyDto in foreignKeys)
    {
      if (foreignKeyDto.Id.HasValue)
      {
        var existingForeignKey = entity.ForeignKeys.FirstOrDefault(fk =>
          fk.Id == foreignKeyDto.Id.Value
        );
        if (existingForeignKey != null)
        {
          existingForeignKey.ConstraintName = foreignKeyDto.ConstraintName;
          existingForeignKey.PrincipalTable = foreignKeyDto.PrincipalTable;

          if (foreignKeyDto.RowVersion != null)
          {
            existingForeignKey.RowVersion = foreignKeyDto.RowVersion;
          }

          UpdateForeignKeyColumns(existingForeignKey, foreignKeyDto.ForeignKeyColumns);
          UpdateForeignKeyPrincipalColumns(existingForeignKey, foreignKeyDto.PrincipalColumns);
        }
      }
      else
      {
        var newForeignKey = new ForeignKeySchema
        {
          ConstraintName = foreignKeyDto.ConstraintName,
          PrincipalTable = foreignKeyDto.PrincipalTable,
          Columns = new List<ForeignKeyColumn>(),
          PrincipalColumns = new List<ForeignKeyPrincipalColumn>(),
        };

        foreach (var columnDto in foreignKeyDto.ForeignKeyColumns)
        {
          newForeignKey.Columns.Add(new ForeignKeyColumn { ColumnName = columnDto.ColumnName });
        }

        foreach (var columnDto in foreignKeyDto.PrincipalColumns)
        {
          newForeignKey.PrincipalColumns.Add(
            new ForeignKeyPrincipalColumn { PrincipalColumnName = columnDto.PrincipalColumnName }
          );
        }

        entity.ForeignKeys.Add(newForeignKey);
      }
    }
  }

  private static void UpdateForeignKeyColumns(
    ForeignKeySchema foreignKey,
    ICollection<UpdateForeignKeyColumnDto> columns
  )
  {
    var columnsToRemove = foreignKey
      .Columns.Where(c => !columns.Any(dto => dto.Id == c.Id))
      .ToList();

    foreach (var column in columnsToRemove)
    {
      foreignKey.Columns.Remove(column);
    }

    foreach (var columnDto in columns)
    {
      if (columnDto.Id.HasValue)
      {
        var existingColumn = foreignKey.Columns.FirstOrDefault(c => c.Id == columnDto.Id.Value);
        if (existingColumn != null)
        {
          existingColumn.ColumnName = columnDto.ColumnName;

          if (columnDto.RowVersion != null)
          {
            existingColumn.RowVersion = columnDto.RowVersion;
          }
        }
      }
      else
      {
        foreignKey.Columns.Add(new ForeignKeyColumn { ColumnName = columnDto.ColumnName });
      }
    }
  }

  private static void UpdateForeignKeyPrincipalColumns(
    ForeignKeySchema foreignKey,
    ICollection<UpdateForeignKeyPrincipalColumnDto> columns
  )
  {
    var columnsToRemove = foreignKey
      .PrincipalColumns.Where(c => !columns.Any(dto => dto.Id == c.Id))
      .ToList();

    foreach (var column in columnsToRemove)
    {
      foreignKey.PrincipalColumns.Remove(column);
    }

    foreach (var columnDto in columns)
    {
      if (columnDto.Id.HasValue)
      {
        var existingColumn = foreignKey.PrincipalColumns.FirstOrDefault(c =>
          c.Id == columnDto.Id.Value
        );
        if (existingColumn != null)
        {
          existingColumn.PrincipalColumnName = columnDto.PrincipalColumnName;
        }
      }
      else
      {
        foreignKey.PrincipalColumns.Add(
          new ForeignKeyPrincipalColumn { PrincipalColumnName = columnDto.PrincipalColumnName }
        );
      }
    }
  }
}
