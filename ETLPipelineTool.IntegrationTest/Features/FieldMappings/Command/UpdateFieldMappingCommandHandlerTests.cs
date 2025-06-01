//using ETLPipelineTool.Application.Features.FieldMappings.Commands;

//namespace ETLPipelineTool.IntegrationTest.Features.FieldMappings.Command
//{
//    public class UpdateFieldMappingCommandHandlerTests
//        : BaseHandlerTest<UpdateFieldMappingCommand, Unit>
//    {
//        [Fact]
//        public async Task TestHandler_WhenInputIsValid_ThenUpdateFieldMapping()
//        {
//            // Arrange
//            var etlPipeline = new EtlPipeline
//            {
//                Name = "Test Pipeline",
//                Description = "Test Description",
//                SourceType = PipelineSourceType.MssqlDatabase,
//                TargetType = PipelineTargetType.MssqlDatabase,
//                SourceConfigurationJson = "Source Config",
//                TargetConfigurationJson = "Target Config",
//                IsActive = true
//            };

//            await Container.AddEntitiesAsync(etlPipeline);

//            var fieldMapping = new FieldMapping
//            {
//                EtlPipelineId = etlPipeline.Id,
//                TargetField = "OriginalTarget",
//                Order = 1
//            };

//            var source = new FieldMappingSource
//            {
//                SourceField = "OriginalSource",
//                Order = 1
//            };

//            var rule = new TransformRule
//            {
//                Sequence = 1,
//                RuleType = TransformRuleType.Identity,
//                RuleConfigurationJson = "{}"
//            };

//            fieldMapping.SourceFields.Add(source);
//            fieldMapping.TransformRules.Add(rule);

//            await Container.AddEntitiesAsync(fieldMapping);
//            await Container.SaveChangesAsync();

//            var updateSources = new List<UpdateFieldMappingSourceCommand>
//            {
//                new UpdateFieldMappingSourceCommand(source.Id, "UpdatedSource", 1)
//            };

//            var updateRules = new List<UpdateTransformRuleCommand>
//            {
//                new UpdateTransformRuleCommand(rule.Id, 1, TransformRuleType.Concatenate, "{\"separator\": \" \"}")
//            };

//            var command = new UpdateFieldMappingCommand(
//                fieldMapping.Id,
//                etlPipeline.Id,
//                "UpdatedTarget",
//                2,
//                updateSources,
//                updateRules,
//                fieldMapping.RowVersion
//            );

//            // Act
//            await Service.Handle(command, new CancellationToken());

//            // Assert
//            var updatedMapping = await Container
//                .GetEntities<FieldMapping>()
//                .Include(x => x.Sources)
//                .Include(x => x.TransformRules)
//                .SingleOrDefaultAsync(x => x.Id == fieldMapping.Id);

//            Assert.NotNull(updatedMapping);
//            Assert.Equal("UpdatedTarget", updatedMapping.TargetField);
//            Assert.Equal(2, updatedMapping.Order);
//            Assert.Equal("UpdatedSource", updatedMapping.Sources.First().SourceField);
//            Assert.Equal(TransformRuleType.Concatenate, updatedMapping.TransformRules.First().RuleType);
//        }
//    }
//}
