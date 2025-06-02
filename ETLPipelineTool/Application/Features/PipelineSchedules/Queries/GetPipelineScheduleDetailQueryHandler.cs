using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using ETLPipelineTool.Application.Features.PipelineSchedules.Projections;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineScheduleDetailQueryHandler(IPipelineScheduleRepository repository)
    : IRequestHandler<GetPipelineScheduleDetailQuery, PipelineScheduleDataDto>
  {
    public async Task<PipelineScheduleDataDto> Handle(
      GetPipelineScheduleDetailQuery request,
      CancellationToken cancellationToken
    )
    {
      return await repository.GetByIdAsync(
        request.Id,
        null,
        PipelineScheduleProjection.AsPipelineScheduleDataDto(),
        cancellationToken
      );
    }
  }
}
