using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
    public class GetEtlPipelinesQueryHandler(IEtlPipelineRepository etlPipelineRepository)
        : GridQueryHandler<GetEtlPipelinesQuery, EtlPipeline, EtlPipelineDataDto>
    {
        protected override List<string> ExpandColumns { get; set; } =
            [
                nameof(EtlPipelineDataDto.Id),
                nameof(EtlPipelineDataDto.RowVersion),
                nameof(EtlPipelineDataDto.SourceType),
            ];

        protected override IQueryable<EtlPipeline> GetBaseQuery(GetEtlPipelinesQuery request)
        {
            return etlPipelineRepository.GetList();
        }

        protected override IQueryable<EtlPipeline> ApplyFiltering(
            IQueryable<EtlPipeline> query,
            GetEtlPipelinesQuery request
        )
        {
            if (request.IsActive is not null)
            {
                query = query.Where(x => x.IsActive == request.IsActive);
            }

            return query;
        }

        protected override Expression<Func<EtlPipeline, EtlPipelineDataDto>> BuildFullProjection()
        {
            return EtlPipelineProjection.AsEtlPipelineDataDto();
        }
    }
}
