using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
  public class GetEtlPipelineDetailQuery(Guid id) : IRequest<EtlPipelineDataDto>
  {
    public Guid Id { get; set; } = id;
  }
}
