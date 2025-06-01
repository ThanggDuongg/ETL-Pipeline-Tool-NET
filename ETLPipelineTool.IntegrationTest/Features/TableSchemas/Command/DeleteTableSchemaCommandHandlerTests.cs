using ETLPipelineTool.Application.Features.TableSchemas.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.TableSchemas.Command
{
  public class DeleteTableSchemaCommandHandlerTests
    : BaseHandlerTest<DeleteTableSchemaCommand, Unit>
  {
    [Fact]
    public async Task TestHandler_WhenInputIsValid_ThenDeleteTableSchema()
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

      TableSchema tableSchema = new()
      {
        EtlPipelineId = etlPipeline.Id,
        TableName = "TableToDelete",
      };

      await Container.AddEntitiesAsync(tableSchema);
      await Container.SaveChangesAsync();
      await Container.ClearChangesAsync();

      DeleteTableSchemaCommand command = new(tableSchema.Id);

      // Act
      await Service.Handle(command, new CancellationToken());

      // Assert
      TableSchema? deletedSchema = await Container
        .GetEntities<TableSchema>()
        .SingleOrDefaultAsync(x => x.Id == tableSchema.Id);

      Assert.Null(deletedSchema);
    }
  }
}
