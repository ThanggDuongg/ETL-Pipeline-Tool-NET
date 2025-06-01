using ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.PipelineSchedules.Command
{
  public class DeletePipelineScheduleCommandHandlerTests
    : BaseHandlerTest<DeletePipelineScheduleCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenDeletePipelineSchedule()
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
        CronExpression = "0 0 * * *",
        IsEnabled = true,
      };

      await Container.AddEntitiesAsync(schedule);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      DeletePipelineScheduleCommand command = new(schedule.Id);

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      PipelineSchedule? deletedSchedule = await Container
        .GetEntities<PipelineSchedule>()
        .SingleOrDefaultAsync(x => x.Id == schedule.Id);

      Assert.Null(deletedSchedule);
    }
  }
}
