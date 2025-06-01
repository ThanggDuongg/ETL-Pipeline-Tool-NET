using ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

namespace ETLPipelineTool.IntegrationTest.Features.PipelineSchedules.Command
{
  public class CreatePipelineScheduleCommandValidatorTests
    : BaseValidatorTest<CreatePipelineScheduleCommand>
  {
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001", "0 */2 * * *", true, null, null, true)]
    public async Task TestValidator(
      string etlPipelineId,
      string cronExpression,
      bool isActive,
      DateTime? startDate,
      DateTime? endDate,
      bool expectedResult
    )
    {
      // Arrange
      CreatePipelineScheduleCommand command = new(
        Guid.Parse(etlPipelineId),
        cronExpression,
        isActive,
        startDate,
        endDate
      );

      // Act
      ValidationFailure[] failures = await ValidateAsync(command);

      // Assert
      Assert.Equal(expectedResult, failures.Length == 0);
    }
  }
}
