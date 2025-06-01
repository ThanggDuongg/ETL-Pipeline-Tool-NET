//using ETLPipelineTool.Application.Features.FieldMappings.Commands;

//namespace ETLPipelineTool.IntegrationTest.Features.FieldMappings.Command
//{
//  public class CreateFieldMappingCommandHandlerTests
//    : BaseHandlerTest<CreateFieldMappingCommand, Unit>
//  {
//    [Fact]
//    public async Task TestHandler_WhenInputIsValid_ThenCreateNewFieldMapping()
//    {
//      // Arrange
//      EtlPipeline etlPipeline = new()
//      {
//        Name = "Test Pipeline",
//        Description = "Test Description",
//        SourceType = PipelineSourceType.MssqlDatabase,
//        TargetType = PipelineTargetType.MssqlDatabase,
//        SourceConfigurationJson = "Source Config",
//        TargetConfigurationJson = "Target Config",
//        IsActive = true,
//      };

//      await Container.AddEntitiesAsync(etlPipeline);
//      await Container.SaveChangesAsync();

//      List<CreateFieldMappingSource> sources = new()
//      {
//        new CreateFieldMappingSourceCommand("SourceField", 1),
//      };

//      List<CreateTransformRuleCommand> transformRules = new()
//      {
//        new CreateTransformRuleCommand(1, TransformRuleType.Trim, "{}"),
//      };

//      var command = new CreateFieldMappingCommand(
//        etlPipeline.Id,
//        "TargetField",
//        1,
//        sources,
//        transformRules
//      );

//      // Act
//      await Service.Handle(command, new CancellationToken());

//      // Assert
//      var fieldMapping = await Container
//        .GetEntities<FieldMapping>()
//        .Include(x => x.SourceFields)
//        .Include(x => x.TransformRules)
//        .SingleOrDefaultAsync(x => x.TargetField == "TargetField");

//      Assert.NotNull(fieldMapping);
//      Assert.Single(fieldMapping.SourceFields);
//      Assert.Single(fieldMapping.TransformRules);
//      Assert.Equal("SourceField", fieldMapping.SourceFields.First().SourceField);
//    }
//  }
//}
