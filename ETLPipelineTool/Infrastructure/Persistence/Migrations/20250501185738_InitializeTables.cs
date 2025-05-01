#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitializeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    ActionType = table.Column<string>(
                        type: "varchar(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    KeyValues = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EtlPipeline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    SourceType = table.Column<string>(
                        type: "varchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    TargetType = table.Column<string>(
                        type: "varchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    SourceConfigurationJson = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    TargetConfigurationJson = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlPipeline", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EtlExecutionLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtlPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(
                        type: "varchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    ErrorMessage = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlExecutionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtlExecutionLog_EtlPipeline_EtlPipelineId",
                        column: x => x.EtlPipelineId,
                        principalTable: "EtlPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "FieldMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtlPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SourceField = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    TargetField = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    TransformExpression = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldMapping_EtlPipeline_EtlPipelineId",
                        column: x => x.EtlPipelineId,
                        principalTable: "EtlPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "PipelineSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtlPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CronExpression = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    IsEnabled = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: true
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineSchedule_EtlPipeline_EtlPipelineId",
                        column: x => x.EtlPipelineId,
                        principalTable: "EtlPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "TransformRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtlPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    RuleType = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    RuleConfigurationJson = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransformRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransformRule_EtlPipeline_EtlPipelineId",
                        column: x => x.EtlPipelineId,
                        principalTable: "EtlPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_EtlExecutionLog_EtlPipelineId",
                table: "EtlExecutionLog",
                column: "EtlPipelineId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_FieldMapping_EtlPipelineId",
                table: "FieldMapping",
                column: "EtlPipelineId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PipelineSchedule_EtlPipelineId",
                table: "PipelineSchedule",
                column: "EtlPipelineId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_TransformRule_EtlPipelineId",
                table: "TransformRule",
                column: "EtlPipelineId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AuditLog");

            migrationBuilder.DropTable(name: "EtlExecutionLog");

            migrationBuilder.DropTable(name: "FieldMapping");

            migrationBuilder.DropTable(name: "PipelineSchedule");

            migrationBuilder.DropTable(name: "TransformRule");

            migrationBuilder.DropTable(name: "EtlPipeline");
        }
    }
}
