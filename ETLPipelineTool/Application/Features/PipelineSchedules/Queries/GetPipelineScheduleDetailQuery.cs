using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineScheduleDetailQuery(Guid id) : IRequest<PipelineScheduleDataDto>
  {
    public Guid Id { get; } = id;
  }
}
