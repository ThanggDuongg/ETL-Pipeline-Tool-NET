#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
  /// <inheritdoc />
  public partial class Alter_PipelineSchedule : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<DateTime>(
        name: "EndDate",
        table: "PipelineSchedule",
        type: "datetime2",
        nullable: true
      );

      migrationBuilder.AddColumn<DateTime>(
        name: "StartDate",
        table: "PipelineSchedule",
        type: "datetime2",
        nullable: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "EndDate", table: "PipelineSchedule");

      migrationBuilder.DropColumn(name: "StartDate", table: "PipelineSchedule");
    }
  }
}
