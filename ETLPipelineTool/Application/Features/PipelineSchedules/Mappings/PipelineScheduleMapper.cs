using ETLPipelineTool.Application.Dtos.V1.Requests.PipelineSchedules;
using ETLPipelineTool.Application.Features.PipelineSchedules.Commands;
using ETLPipelineTool.Application.Features.PipelineSchedules.Queries;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Mappings
{
  public static class PipelineScheduleMapper
  {
    public static GetPipelineSchedulesQuery ToGetPipelineSchedulesQuery(
      GridPipelineSchedulesFilterDto dto
    )
    {
      return new GetPipelineSchedulesQuery(
        dto.GridDataSourceDto.Take,
        dto.GridDataSourceDto.Skip,
        dto.GridDataSourceDto.PreloadAllData,
        dto.GridDataSourceDto.SortFields,
        dto.EtlPipelineId,
        dto.IsActive
      );
    }

    public static GetPipelineScheduleDetailQuery ToGetPipelineScheduleDetailQuery(Guid id)
    {
      return new GetPipelineScheduleDetailQuery(id);
    }

    public static DeletePipelineScheduleCommand ToDeletePipelineScheduleCommand(Guid id)
    {
      return new DeletePipelineScheduleCommand(id);
    }

    public static UpdatePipelineScheduleCommand ToUpdatePipelineScheduleCommand(
      UpdatePipelineScheduleDto dto
    )
    {
      return new UpdatePipelineScheduleCommand(
        dto.Id,
        dto.EtlPipelineId,
        dto.CronExpression,
        dto.IsActive,
        dto.StartDate,
        dto.EndDate,
        dto.RowVersion
      );
    }

    public static CreatePipelineScheduleCommand ToCreatePipelineScheduleCommand(
      CreatePipelineScheduleDto dto
    )
    {
      return new CreatePipelineScheduleCommand(
        dto.EtlPipelineId,
        dto.CronExpression,
        dto.IsActive,
        dto.StartDate,
        dto.EndDate
      );
    }

    public static PipelineSchedule ToEntity(CreatePipelineScheduleCommand command)
    {
      return new PipelineSchedule
      {
        EtlPipelineId = command.EtlPipelineId,
        CronExpression = command.CronExpression,
        IsEnabled = command.IsActive,
        StartDate = command.StartDate,
        EndDate = command.EndDate,
      };
    }

    public static void UpdateEntity(PipelineSchedule entity, UpdatePipelineScheduleCommand command)
    {
      entity.EtlPipelineId = command.EtlPipelineId;
      entity.CronExpression = command.CronExpression;
      entity.IsEnabled = command.IsActive;
      entity.StartDate = command.StartDate;
      entity.EndDate = command.EndDate;
    }
  }
}
