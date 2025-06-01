using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;
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
        EtlPipeline = new EtlPipelineDataDto
        {
          Id = x.EtlPipelineId,
          Name = x.EtlPipeline!.Name,
          Description = x.EtlPipeline.Description,
          SourceType = x.EtlPipeline.SourceType,
          TargetType = x.EtlPipeline.TargetType,
          SourceConfigurationJson = x.EtlPipeline.SourceConfigurationJson,
          TargetConfigurationJson = x.EtlPipeline.TargetConfigurationJson,
          IsActive = x.EtlPipeline.IsActive,
          RowVersion = x.EtlPipeline.RowVersion,
        },
        CronExpression = x.CronExpression,
        IsActive = x.IsEnabled,
        StartDate = x.StartDate,
        EndDate = x.EndDate,
        RowVersion = x.RowVersion,
      };
    }
  }
}
