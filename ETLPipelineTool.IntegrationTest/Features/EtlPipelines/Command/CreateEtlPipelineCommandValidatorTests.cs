namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
    public class CreateEtlPipelineCommandValidatorTests
        : BaseValidatorTest<CreateEtlPipelineCommand>
    {
        [Theory]
        [InlineData(
            "Name",
            "Description",
            "Source",
            "Target",
            PipelineSourceType.SqlDatabase,
            PipelineTargetType.SqlDatabase,
            true
        )]
        public async Task TestValidator(
            string name,
            string description,
            string sourceConfigurationJson,
            string targetConfigurationJson,
            PipelineSourceType pipelineSourceType,
            PipelineTargetType pipelineTargetType,
            bool expectedResult
        )
        {
            // Arrange
            var command = new CreateEtlPipelineCommand(
                name,
                description,
                pipelineSourceType,
                pipelineTargetType,
                sourceConfigurationJson,
                targetConfigurationJson,
                true
            );

            // Act
            var failures = await ValidateAsync(command);

            // Assert
            Assert.Equal(expectedResult, failures.Length == 0);
        }
    }
}
