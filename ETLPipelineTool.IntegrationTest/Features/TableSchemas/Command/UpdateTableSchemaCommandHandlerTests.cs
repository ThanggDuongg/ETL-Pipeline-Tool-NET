//namespace ETLPipelineTool.IntegrationTest.Features.TableSchemas.Command
//{
//    public class UpdateTableSchemaCommandHandlerTests
//        : BaseHandlerTest<UpdateTableSchemaCommand, Unit>
//    {
//        [Fact]
//        public async Task TestHandler_WhenInputIsValid_ThenUpdateTableSchema()
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

//            var tableSchema = new TableSchema
//            {
//                EtlPipelineId = etlPipeline.Id,
//                TableName = "OriginalTable"
//            };

//            var column = new TableColumn
//            {
//                ColumnName = "OriginalColumn",
//                DataType = "varchar",
//                IsPrimaryKey = false,
//                IsNullable = true,
//                MaxLength = 50
//            };

//            tableSchema.AddColumn(column);
//            await Container.AddEntitiesAsync(tableSchema);
//            await Container.SaveChangesAsync();

//            var updateColumns = new List<UpdateTableColumnCommand>
//            {
//                new UpdateTableColumnCommand(column.Id, "UpdatedColumn", "nvarchar", false, false, 100, null)
//            };

//            var command = new UpdateTableSchemaCommand(
//                tableSchema.Id,
//                etlPipeline.Id,
//                "UpdatedTable",
//                updateColumns,
//                new List<UpdateForeignKeyCommand>(),
//                tableSchema.RowVersion
//            );

//            // Act
//            await Service.Handle(command, new CancellationToken());

//            // Assert
//            var updatedSchema = await Container
//                .GetEntities<TableSchema>()
//                .Include(x => x.Columns)
//                .SingleOrDefaultAsync(x => x.Id == tableSchema.Id);

//            Assert.NotNull(updatedSchema);
//            Assert.Equal("UpdatedTable", updatedSchema.TableName);
//            Assert.Equal("UpdatedColumn", updatedSchema.Columns.First().ColumnName);
//        }
//    }
//}
