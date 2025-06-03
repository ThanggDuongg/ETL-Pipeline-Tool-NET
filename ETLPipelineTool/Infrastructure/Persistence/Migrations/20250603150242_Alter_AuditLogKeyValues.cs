#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
  /// <inheritdoc />
  public partial class Alter_AuditLogKeyValues : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "KeyValues",
        table: "AuditLog",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "KeyValues",
        table: "AuditLog",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );
    }
  }
}
