using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class CreateTableSchemaCommandValidator : AbstractValidator<CreateTableSchemaCommand>
{
  public CreateTableSchemaCommandValidator()
  {
    RuleFor(x => x.TableSchema).NotNull();
    RuleFor(x => x.TableSchema.TableName).NotEmpty();
    RuleFor(x => x.TableSchema.EtlPipelineId).NotEmpty();

    RuleFor(x => x.TableSchema.Columns).NotNull();
    RuleForEach(x => x.TableSchema.Columns).SetValidator(new CreateColumnSchemaValidator());

    RuleForEach(x => x.TableSchema.ForeignKeys).SetValidator(new CreateForeignKeySchemaValidator());
  }
}

public class CreateColumnSchemaValidator : AbstractValidator<CreateColumnSchemaDto>
{
  public CreateColumnSchemaValidator()
  {
    RuleFor(x => x.ColumnName).NotEmpty();
    RuleFor(x => x.DataType).NotEmpty();
  }
}

public class CreateForeignKeySchemaValidator : AbstractValidator<CreateForeignKeySchemaDto>
{
  public CreateForeignKeySchemaValidator()
  {
    RuleFor(x => x.ConstraintName).NotEmpty();
    RuleFor(x => x.PrincipalTable).NotEmpty();

    // Validate foreign key columns
    RuleFor(x => x.ForeignKeyColumns).NotNull().NotEmpty();
    RuleForEach(x => x.ForeignKeyColumns).SetValidator(new CreateForeignKeyColumnValidator());

    // Validate principal columns
    RuleFor(x => x.PrincipalColumns).NotNull().NotEmpty();
    RuleForEach(x => x.PrincipalColumns)
      .SetValidator(new CreateForeignKeyPrincipalColumnValidator());
  }
}

public class CreateForeignKeyColumnValidator : AbstractValidator<CreateForeignKeyColumnDto>
{
  public CreateForeignKeyColumnValidator()
  {
    RuleFor(x => x.ColumnName).NotEmpty();
  }
}

public class CreateForeignKeyPrincipalColumnValidator
  : AbstractValidator<CreateForeignKeyPrincipalColumnDto>
{
  public CreateForeignKeyPrincipalColumnValidator()
  {
    RuleFor(x => x.PrincipalColumnName).NotEmpty();
  }
}
