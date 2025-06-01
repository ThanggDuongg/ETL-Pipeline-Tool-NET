using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using ETLPipelineTool.Application.Features.PipelineSchedules.Projections;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineScheduleDetailQueryHandler
    : IRequestHandler<GetPipelineScheduleDetailQuery, PipelineScheduleDataDto>
  {
    private readonly IPipelineScheduleRepository _repository;

    public GetPipelineScheduleDetailQueryHandler(IPipelineScheduleRepository repository)
    {
      _repository = repository;
    }

    public async Task<PipelineScheduleDataDto> Handle(
      GetPipelineScheduleDetailQuery request,
      CancellationToken cancellationToken
    )
    {
      var data = await _repository.GetByIdAsync(
        request.Id,
        null,
        PipelineScheduleProjection.AsPipelineScheduleDataDto(),
        cancellationToken
      );

      return data;
    }
  }
}
