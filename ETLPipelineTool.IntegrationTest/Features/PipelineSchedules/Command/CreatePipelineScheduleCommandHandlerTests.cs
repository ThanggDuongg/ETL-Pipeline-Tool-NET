using ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.PipelineSchedules.Command
{
  public class CreatePipelineScheduleCommandHandlerTests
    : BaseHandlerTest<CreatePipelineScheduleCommand, Guid>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenCreateNewPipelineSchedule()
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
      await Container.SaveChangesAsync();

      CreatePipelineScheduleCommand command = new(
        etlPipeline.Id,
        "0 */2 * * *", // Every 2 hours
        true,
        null,
        null
      );

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      PipelineSchedule? schedule = await Container
        .GetEntities<PipelineSchedule>()
        .SingleOrDefaultAsync(x => x.EtlPipelineId == etlPipeline.Id);

      Assert.NotNull(schedule);
      Assert.Equal("0 */2 * * *", schedule.CronExpression);
      Assert.True(schedule.IsEnabled);
    }
  }
}
