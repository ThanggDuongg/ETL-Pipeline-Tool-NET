using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class UpdateTableSchemaCommandValidator : AbstractValidator<UpdateTableSchemaCommand>
{
  public UpdateTableSchemaCommandValidator()
  {
    RuleFor(x => x.TableSchema).NotNull();
    RuleFor(x => x.TableSchema.Id).NotEmpty();
    RuleFor(x => x.TableSchema.TableName).NotEmpty();
    RuleFor(x => x.TableSchema.RowVersion).NotNull();

    RuleFor(x => x.TableSchema.Columns).NotNull();
    RuleForEach(x => x.TableSchema.Columns).SetValidator(new UpdateColumnSchemaValidator());

    RuleForEach(x => x.TableSchema.ForeignKeys).SetValidator(new UpdateForeignKeySchemaValidator());
  }
}

public class UpdateColumnSchemaValidator : AbstractValidator<UpdateColumnSchemaDto>
{
  public UpdateColumnSchemaValidator()
  {
    RuleFor(x => x.ColumnName).NotEmpty();
    RuleFor(x => x.DataType).NotEmpty();

    When(
      x => x.Id.HasValue,
      () =>
      {
        RuleFor(x => x.RowVersion).NotNull();
      }
    );
  }
}

public class UpdateForeignKeySchemaValidator : AbstractValidator<UpdateForeignKeySchemaDto>
{
  public UpdateForeignKeySchemaValidator()
  {
    RuleFor(x => x.ConstraintName).NotEmpty();
    RuleFor(x => x.PrincipalTable).NotEmpty();

    RuleFor(x => x.ForeignKeyColumns).NotNull().NotEmpty();
    RuleForEach(x => x.ForeignKeyColumns).SetValidator(new UpdateForeignKeyColumnValidator());

    RuleFor(x => x.PrincipalColumns).NotNull().NotEmpty();
    RuleForEach(x => x.PrincipalColumns)
      .SetValidator(new UpdateForeignKeyPrincipalColumnValidator());

    When(
      x => x.Id.HasValue,
      () =>
      {
        RuleFor(x => x.RowVersion).NotNull();
      }
    );
  }
}

public class UpdateForeignKeyColumnValidator : AbstractValidator<UpdateForeignKeyColumnDto>
{
  public UpdateForeignKeyColumnValidator()
  {
    RuleFor(x => x.ColumnName).NotEmpty();

    When(
      x => x.Id.HasValue,
      () =>
      {
        RuleFor(x => x.RowVersion).NotNull();
      }
    );
  }
}

public class UpdateForeignKeyPrincipalColumnValidator
  : AbstractValidator<UpdateForeignKeyPrincipalColumnDto>
{
  public UpdateForeignKeyPrincipalColumnValidator()
  {
    RuleFor(x => x.PrincipalColumnName).NotEmpty();

    When(
      x => x.Id.HasValue,
      () =>
      {
        RuleFor(x => x.RowVersion).NotNull();
      }
    );
  }
}
