namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
    public class GetEtlPipelineDetailQueryValidator : AbstractValidator<GetEtlPipelineDetailQuery>
    {
        public GetEtlPipelineDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
