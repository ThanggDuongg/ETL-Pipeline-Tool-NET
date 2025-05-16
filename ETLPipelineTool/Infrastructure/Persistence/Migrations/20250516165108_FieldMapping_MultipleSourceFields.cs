using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FieldMapping_MultipleSourceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceField",
                table: "FieldMapping");

            migrationBuilder.RenameColumn(
                name: "TransformExpression",
                table: "FieldMapping",
                newName: "TransformConfig");

            migrationBuilder.AddColumn<string>(
                name: "SourceFields",
                table: "FieldMapping",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransformRuleType",
                table: "FieldMapping",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceFields",
                table: "FieldMapping");

            migrationBuilder.DropColumn(
                name: "TransformRuleType",
                table: "FieldMapping");

            migrationBuilder.RenameColumn(
                name: "TransformConfig",
                table: "FieldMapping",
                newName: "TransformExpression");

            migrationBuilder.AddColumn<string>(
                name: "SourceField",
                table: "FieldMapping",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
