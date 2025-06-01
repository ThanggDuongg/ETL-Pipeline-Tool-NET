namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
  public class UpdateEtlPipelineCommandValidatorTests : BaseValidatorTest<UpdateEtlPipelineCommand>
  {
    [Theory]
    [InlineData(
      "00000000-0000-0000-0000-000000000001",
      "Updated Name",
      "Updated Description",
      "Source",
      "Target",
      PipelineSourceType.MssqlDatabase,
      PipelineTargetType.MssqlDatabase,
      true,
      "AAAAAAAAB9E=",
      true
    )]
    public async Task TestValidator(
      string id,
      string name,
      string description,
      string sourceConfigurationJson,
      string targetConfigurationJson,
      PipelineSourceType pipelineSourceType,
      PipelineTargetType pipelineTargetType,
      bool isActive,
      string rowVersion,
      bool expectedResult
    )
    {
      // Arrange
      UpdateEtlPipelineCommand command = new(
        Guid.Parse(id),
        name,
        description,
        pipelineSourceType,
        pipelineTargetType,
        sourceConfigurationJson,
        targetConfigurationJson,
        isActive,
        Convert.FromBase64String(rowVersion)
      );

      // Act
      ValidationFailure[] failures = await ValidateAsync(command);

      // Assert
      Assert.Equal(expectedResult, failures.Length == 0);
    }
  }
}
