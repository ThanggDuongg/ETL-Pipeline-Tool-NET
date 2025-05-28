#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EtlExecutionLog_UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorDetails",
                table: "EtlExecutionLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<double>(
                name: "ProcessingTimeMs",
                table: "EtlExecutionLog",
                type: "float",
                nullable: false,
                defaultValue: 0.0
            );

            migrationBuilder.AddColumn<int>(
                name: "RecordsFailed",
                table: "EtlExecutionLog",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "RecordsProcessed",
                table: "EtlExecutionLog",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "RecordsSucceeded",
                table: "EtlExecutionLog",
                type: "int",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ErrorDetails", table: "EtlExecutionLog");

            migrationBuilder.DropColumn(name: "ProcessingTimeMs", table: "EtlExecutionLog");

            migrationBuilder.DropColumn(name: "RecordsFailed", table: "EtlExecutionLog");

            migrationBuilder.DropColumn(name: "RecordsProcessed", table: "EtlExecutionLog");

            migrationBuilder.DropColumn(name: "RecordsSucceeded", table: "EtlExecutionLog");
        }
    }
}
