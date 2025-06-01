using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using MediatR;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineScheduleDetailQuery : IRequest<PipelineScheduleDataDto>
  {
    public Guid Id { get; set; }

    public GetPipelineScheduleDetailQuery(Guid id)
    {
      Id = id;
    }
  }
}
