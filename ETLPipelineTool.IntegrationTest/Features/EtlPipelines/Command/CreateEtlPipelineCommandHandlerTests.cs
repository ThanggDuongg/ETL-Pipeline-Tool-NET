using ETLPipelineTool.Domain.Enums;

namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
    public class CreateEtlPipelineCommandHandlerTests
        : BaseHandlerTest<CreateEtlPipelineCommand, Unit>
    {
        [Fact]
        public async Task TestHandler_WhenInputIsValid_ThenCreateNewEtlPipeline()
        {
            // Arrange
            var command = new CreateEtlPipelineCommand(
                "Etl Pipeline Name",
                "Etl Pipeline Description",
                PipelineSourceType.ExcelFile,
                PipelineTargetType.SqlDatabase,
                "Source Configuration Json",
                "Target Configuration Json",
                true
            );

            // Act
            await Service.Handle(command, new CancellationToken());

            // Assert
            var etlPipeline = await Container
                .GetEntities<EtlPipeline>()
                .SingleOrDefaultAsync(x => x.Name == command.Name);
            Assert.NotNull(etlPipeline);
        }
    }
}
