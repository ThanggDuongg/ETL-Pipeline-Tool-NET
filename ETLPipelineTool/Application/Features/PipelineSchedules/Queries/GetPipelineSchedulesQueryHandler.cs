using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using ETLPipelineTool.Application.Features.PipelineSchedules.Projections;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineSchedulesQueryHandler(IPipelineScheduleRepository repository)
    : GridQueryHandler<GetPipelineSchedulesQuery, PipelineSchedule, PipelineScheduleDataDto>
  {
    protected override IQueryable<PipelineSchedule> GetBaseQuery(GetPipelineSchedulesQuery request)
    {
      return repository.GetList().Include(x => x.EtlPipeline);
    }

    protected override IQueryable<PipelineSchedule> ApplyFiltering(
      IQueryable<PipelineSchedule> query,
      GetPipelineSchedulesQuery request
    )
    {
      if (request.EtlPipelineId.HasValue)
      {
        query = query.Where(x => x.EtlPipelineId == request.EtlPipelineId.Value);
      }

      if (request.IsActive.HasValue)
      {
        query = query.Where(x => x.IsEnabled == request.IsActive.Value);
      }

      return query;
    }

    protected override Expression<
      Func<PipelineSchedule, PipelineScheduleDataDto>
    > BuildFullProjection()
    {
      return PipelineScheduleProjection.AsPipelineScheduleDataDto();
    }
  }
}
