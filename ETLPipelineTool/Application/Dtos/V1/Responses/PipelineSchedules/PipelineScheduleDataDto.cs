namespace ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules
{
  public record PipelineScheduleDataDto
  {
    public Guid Id { get; init; }
    public Guid EtlPipelineId { get; init; }
    public string CronExpression { get; init; } = default!;
    public bool IsActive { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public byte[]? RowVersion { get; init; }

    public PipelineScheduleDataDto() { }

    public PipelineScheduleDataDto(
      Guid id,
      Guid etlPipelineId,
      string cronExpression,
      bool isActive,
      DateTime? startDate,
      DateTime? endDate,
      byte[]? rowVersion
    )
    {
      Id = id;
      EtlPipelineId = etlPipelineId;
      CronExpression = cronExpression;
      IsActive = isActive;
      StartDate = startDate;
      EndDate = endDate;
      RowVersion = rowVersion;
    }
  }
}
