using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
    public class GetEtlPipelineDetailQueryHandler(IEtlPipelineRepository etlPipelineRepository)
        : IRequestHandler<GetEtlPipelineDetailQuery, EtlPipelineDataDto>
    {
        private readonly IEtlPipelineRepository _etlPipelineRepository = etlPipelineRepository;

        public async Task<EtlPipelineDataDto> Handle(
            GetEtlPipelineDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            var data = await _etlPipelineRepository.GetByIdAsync(
                request.Id,
                x => x.IsActive,
                x => EtlPipelineMapper.ToEtlPipelineDataDto(x),
                cancellationToken
            );
            return data;
        }
    }
}
