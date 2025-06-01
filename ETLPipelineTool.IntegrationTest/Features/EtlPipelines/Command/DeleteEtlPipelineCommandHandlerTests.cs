namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
  public class DeleteEtlPipelineCommandHandlerTests
    : BaseHandlerTest<DeleteEtlPipelineCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenDeleteEtlPipeline()
    {
      // Arrange
      EtlPipeline existingPipeline = new()
      {
        Name = "Pipeline To Delete",
        Description = "Description",
        SourceType = PipelineSourceType.MssqlDatabase,
        TargetType = PipelineTargetType.MssqlDatabase,
        SourceConfigurationJson = "Source Config",
        TargetConfigurationJson = "Target Config",
        IsActive = true,
      };

      await Container.AddEntitiesAsync(existingPipeline);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      DeleteEtlPipelineCommand command = new(existingPipeline.Id);

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      EtlPipeline? deletedPipeline = await Container
        .GetEntities<EtlPipeline>()
        .SingleOrDefaultAsync(x => x.Id == existingPipeline.Id);

      Assert.Null(deletedPipeline);
    }
  }
}
