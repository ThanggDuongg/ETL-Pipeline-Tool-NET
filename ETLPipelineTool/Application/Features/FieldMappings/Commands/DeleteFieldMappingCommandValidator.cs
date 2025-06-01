namespace ETLPipelineTool.Application.Features.FieldMappings.Commands;

public class DeleteFieldMappingCommandValidator : AbstractValidator<DeleteFieldMappingCommand>
{
  public DeleteFieldMappingCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
