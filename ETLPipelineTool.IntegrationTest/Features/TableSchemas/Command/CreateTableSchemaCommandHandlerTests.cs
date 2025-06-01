//using ETLPipelineTool.Application.Features.TableSchemas.Commands;

//namespace ETLPipelineTool.IntegrationTest.Features.TableSchemas.Command
//{
//    public class CreateTableSchemaCommandHandlerTests
//        : BaseHandlerTest<CreateTableSchemaCommand, Unit>
//    {
//        [Fact]
//        public async Task TestHandler_WhenInputIsValid_ThenCreateNewTableSchema()
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
//            await Container.SaveChangesAsync();

//            var columns = new List<CreateTableColumnCommand>
//            {
//                new CreateTableColumnCommand("Id", "int", true, false, null, null),
//                new CreateTableColumnCommand("Name", "nvarchar", false, false, 100, null)
//            };

//            var command = new CreateTableSchemaCommand(
//                etlPipeline.Id,
//                "TestTable",
//                columns,
//                new List<CreateForeignKeyCommand>()
//            );

//            // Act
//            await Service.Handle(command, new CancellationToken());

//            // Assert
//            var tableSchema = await Container
//                .GetEntities<TableSchema>()
//                .Include(x => x.Columns)
//                .SingleOrDefaultAsync(x => x.TableName == "TestTable");

//            Assert.NotNull(tableSchema);
//            Assert.Equal(2, tableSchema.Columns.Count);
//            Assert.Equal("Id", tableSchema.Columns.First().ColumnName);
//        }
//    }
//}
