//namespace ETLPipelineTool.IntegrationTest.Features.FieldMappings.Command
//{
//    public class CreateFieldMappingCommandValidatorTests
//        : BaseValidatorTest<CreateFieldMappingCommand>
//    {
//        [Theory]
//        [InlineData(
//            "00000000-0000-0000-0000-000000000001",
//            "TargetField",
//            1,
//            true
//        )]
//        [InlineData(
//            "00000000-0000-0000-0000-000000000001",
//            "",
//            1,
//            false
//        )]
//        public async Task TestValidator(
//            string etlPipelineId,
//            string targetField,
//            int order,
//            bool expectedResult
//        )
//        {
//            // Arrange
//            var sources = new List<CreateFieldMappingSourceCommand>
//            {
//                new CreateFieldMappingSourceCommand("SourceField", 1)
//            };

//            var transformRules = new List<CreateTransformRuleCommand>
//            {
//                new CreateTransformRuleCommand(1, TransformRuleType.Trim, "{}")
//            };

//            var command = new CreateFieldMappingCommand(
//                Guid.Parse(etlPipelineId),
//                targetField,
//                order,
//                sources,
//                transformRules
//            );

//            // Act
//            var failures = await ValidateAsync(command);

//            // Assert
//            Assert.Equal(expectedResult, failures.Length == 0);
//        }
//    }
//}
