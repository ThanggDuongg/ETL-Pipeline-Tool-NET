using FluentValidation;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemaDetailQueryValidator : AbstractValidator<GetTableSchemaDetailQuery>
{
  public GetTableSchemaDetailQueryValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
