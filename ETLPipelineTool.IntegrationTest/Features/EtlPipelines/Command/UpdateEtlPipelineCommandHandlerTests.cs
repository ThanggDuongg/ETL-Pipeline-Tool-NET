namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
  public class UpdateEtlPipelineCommandHandlerTests
    : BaseHandlerTest<UpdateEtlPipelineCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenUpdateEtlPipeline()
    {
      // Arrange
      EtlPipeline existingPipeline = new()
      {
        Name = "Original Name",
        Description = "Original Description",
        SourceType = PipelineSourceType.MssqlDatabase,
        TargetType = PipelineTargetType.MssqlDatabase,
        SourceConfigurationJson = "Original Source Config",
        TargetConfigurationJson = "Original Target Config",
        IsActive = true,
      };

      await Container.AddEntitiesAsync(existingPipeline);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      UpdateEtlPipelineCommand command = new(
        existingPipeline.Id,
        "Updated Name",
        "Updated Description",
        PipelineSourceType.MssqlDatabase,
        PipelineTargetType.MssqlDatabase,
        "Updated Source Config",
        "Updated Target Config",
        true,
        existingPipeline.RowVersion!
      );

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      EtlPipeline? updatedPipeline = await Container
        .GetEntities<EtlPipeline>()
        .SingleOrDefaultAsync(x => x.Id == existingPipeline.Id);

      Assert.NotNull(updatedPipeline);
      Assert.Equal("Updated Name", updatedPipeline.Name);
      Assert.Equal("Updated Description", updatedPipeline.Description);
      Assert.Equal("Updated Source Config", updatedPipeline.SourceConfigurationJson);
      Assert.Equal("Updated Target Config", updatedPipeline.TargetConfigurationJson);
    }
  }
}
