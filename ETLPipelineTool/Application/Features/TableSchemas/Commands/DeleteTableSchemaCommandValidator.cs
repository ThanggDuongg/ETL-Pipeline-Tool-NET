namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class DeleteTableSchemaCommandValidator : AbstractValidator<DeleteTableSchemaCommand>
{
  public DeleteTableSchemaCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
