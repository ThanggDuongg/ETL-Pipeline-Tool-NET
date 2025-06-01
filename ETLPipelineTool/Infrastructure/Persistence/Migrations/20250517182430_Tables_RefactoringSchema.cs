#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
  /// <inheritdoc />
  public partial class Tables_RefactoringSchema : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_TransformRule_EtlPipeline_EtlPipelineId",
        table: "TransformRule"
      );

      migrationBuilder.DropColumn(name: "SourceFields", table: "FieldMapping");

      migrationBuilder.DropColumn(name: "TransformConfig", table: "FieldMapping");

      migrationBuilder.DropColumn(name: "TransformRuleType", table: "FieldMapping");

      migrationBuilder.RenameColumn(
        name: "EtlPipelineId",
        table: "TransformRule",
        newName: "FieldMappingId"
      );

      migrationBuilder.RenameIndex(
        name: "IX_TransformRule_EtlPipelineId",
        table: "TransformRule",
        newName: "IX_TransformRule_FieldMappingId"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CronExpression",
        table: "PipelineSchedule",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(20)",
        oldMaxLength: 20
      );

      migrationBuilder.CreateTable(
        name: "FieldMappingSource",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          FieldMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Order = table.Column<int>(type: "int", nullable: false),
          SourceField = table.Column<string>(type: "nvarchar(max)", nullable: false),
          RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
          CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
          CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
          ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_FieldMappingSource", x => x.Id);
          table.ForeignKey(
            name: "FK_FieldMappingSource_FieldMapping_FieldMappingId",
            column: x => x.FieldMappingId,
            principalTable: "FieldMapping",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_FieldMappingSource_FieldMappingId",
        table: "FieldMappingSource",
        column: "FieldMappingId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_TransformRule_FieldMapping_FieldMappingId",
        table: "TransformRule",
        column: "FieldMappingId",
        principalTable: "FieldMapping",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_TransformRule_FieldMapping_FieldMappingId",
        table: "TransformRule"
      );

      migrationBuilder.DropTable(name: "FieldMappingSource");

      migrationBuilder.RenameColumn(
        name: "FieldMappingId",
        table: "TransformRule",
        newName: "EtlPipelineId"
      );

      migrationBuilder.RenameIndex(
        name: "IX_TransformRule_FieldMappingId",
        table: "TransformRule",
        newName: "IX_TransformRule_EtlPipelineId"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CronExpression",
        table: "PipelineSchedule",
        type: "nvarchar(20)",
        maxLength: 20,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AddColumn<string>(
        name: "SourceFields",
        table: "FieldMapping",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "TransformConfig",
        table: "FieldMapping",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "TransformRuleType",
        table: "FieldMapping",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddForeignKey(
        name: "FK_TransformRule_EtlPipeline_EtlPipelineId",
        table: "TransformRule",
        column: "EtlPipelineId",
        principalTable: "EtlPipeline",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }
  }
}
