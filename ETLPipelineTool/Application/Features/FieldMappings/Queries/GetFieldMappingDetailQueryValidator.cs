namespace ETLPipelineTool.Application.Features.FieldMappings.Queries
{
  public class GetFieldMappingDetailQueryValidator : AbstractValidator<GetFieldMappingDetailQuery>
  {
    public GetFieldMappingDetailQueryValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
