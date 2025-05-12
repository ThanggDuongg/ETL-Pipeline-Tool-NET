using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
    public class GetEtlPipelineDetailQueryHandler(IEtlPipelineRepository etlPipelineRepository)
        : IRequestHandler<GetEtlPipelineDetailQuery, EtlPipelineDataDto>
    {
        public async Task<EtlPipelineDataDto> Handle(
            GetEtlPipelineDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            var data = await etlPipelineRepository.GetByIdAsync(
                request.Id,
                null,
                EtlPipelineProjection.AsEtlPipelineDataDto(),
                cancellationToken
            );
            return data;
        }
    }
}
