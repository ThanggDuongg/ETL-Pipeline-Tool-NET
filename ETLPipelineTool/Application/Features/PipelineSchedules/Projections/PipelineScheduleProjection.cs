using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Projections
{
  public static class PipelineScheduleProjection
  {
    public static Expression<
      Func<PipelineSchedule, PipelineScheduleDataDto>
    > AsPipelineScheduleDataDto()
    {
      return x => new PipelineScheduleDataDto
      {
        Id = x.Id,
        EtlPipelineId = x.EtlPipelineId,
        CronExpression = x.CronExpression,
        IsActive = x.IsEnabled,
        StartDate = x.StartDate,
        EndDate = x.EndDate,
        RowVersion = x.RowVersion,
      };
    }
  }
}
