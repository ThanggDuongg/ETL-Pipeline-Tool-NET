using ETLPipelineTool.Application.Features.FieldMappings.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.FieldMappings.Command
{
  public class DeleteFieldMappingCommandHandlerTests
    : BaseHandlerTest<DeleteFieldMappingCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenDeleteFieldMapping()
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

      FieldMapping fieldMapping = new()
      {
        EtlPipelineId = etlPipeline.Id,
        TargetField = "TargetToDelete",
        Order = 1,
      };

      await Container.AddEntitiesAsync(fieldMapping);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      DeleteFieldMappingCommand command = new(fieldMapping.Id);

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      FieldMapping? deletedMapping = await Container
        .GetEntities<FieldMapping>()
        .SingleOrDefaultAsync(x => x.Id == fieldMapping.Id);

      Assert.Null(deletedMapping);
    }
  }
}
