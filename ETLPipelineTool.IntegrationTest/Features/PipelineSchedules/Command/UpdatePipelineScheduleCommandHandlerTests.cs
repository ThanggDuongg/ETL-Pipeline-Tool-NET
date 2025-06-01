using ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.PipelineSchedules.Command
{
  public class UpdatePipelineScheduleCommandHandlerTests
    : BaseHandlerTest<UpdatePipelineScheduleCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenUpdatePipelineSchedule()
    {
      // Arrange
      EtlPipeline etlPipeline = new()
      {
        Name = "Test Pipeline",
        Description = "Test Description",
        SourceType = PipelineSourceType.MssqlDatabase,
        TargetType = PipelineTargetType.MssqlDatabase,
        SourceConfigurationJson = "Source Config",
        TargetConfigurationJson = "Target Config",
        IsActive = true,
      };

      await Container.AddEntitiesAsync(etlPipeline);

      PipelineSchedule schedule = new()
      {
        EtlPipelineId = etlPipeline.Id,
        CronExpression = "0 0 * * *", // Daily at midnight
        IsEnabled = true,
      };

      await Container.AddEntitiesAsync(schedule);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      UpdatePipelineScheduleCommand command = new(
        schedule.Id,
        etlPipeline.Id,
        "0 */4 * * *", // Every 4 hours
        false,
        null,
        null,
        schedule.RowVersion!
      );

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      PipelineSchedule? updatedSchedule = await Container
        .GetEntities<PipelineSchedule>()
        .SingleOrDefaultAsync(x => x.Id == schedule.Id);

      Assert.NotNull(updatedSchedule);
      Assert.Equal("0 */4 * * *", updatedSchedule.CronExpression);
      Assert.False(updatedSchedule.IsEnabled);
    }
  }
}
