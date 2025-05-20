namespace ETLPipelineTool.IntegrationTest.Features.EtlPipelines.Command
{
    public class CreateEtlPipelineCommandHandlerTests
        : BaseHandlerTest<CreateEtlPipelineCommand, Guid>
    {
        [Fact]
        public async Task TestHandler_WhenInputIsValid_ThenCreateNewEtlPipeline()
        {
            // Arrange
            var command = new CreateEtlPipelineCommand(
                "Etl Pipeline Name",
                "Etl Pipeline Description",
                PipelineSourceType.MssqlDatabase,
                PipelineTargetType.MssqlDatabase,
                "Source Configuration Json",
                "Target Configuration Json",
                true
            );

            // Act
            var id = await Service.Handle(command, new CancellationToken());

            // Assert
            var etlPipeline = await Container
                .GetEntities<EtlPipeline>()
                .SingleOrDefaultAsync(x => x.Name == command.Name);
            Assert.NotEmpty(id.ToString());
            Assert.NotNull(etlPipeline);
        }
    }
}
