#nullable disable

namespace ETLPipelineTool.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Tables_AddSchemaMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EtlPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: true
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableSchema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableSchema_EtlPipeline_EtlPipelineId",
                        column: x => x.EtlPipelineId,
                        principalTable: "EtlPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ColumnSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableSchemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColumnName = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    DataType = table.Column<string>(
                        type: "varchar(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    IsPrimaryKey = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: false
                    ),
                    IsNullable = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: false
                    ),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: true
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnSchema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnSchema_TableSchema_TableSchemaId",
                        column: x => x.TableSchemaId,
                        principalTable: "TableSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ForeignKeySchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableSchemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstraintName = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    PrincipalTable = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: true
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignKeySchema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForeignKeySchema_TableSchema_TableSchemaId",
                        column: x => x.TableSchemaId,
                        principalTable: "TableSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ForeignKeyColumn",
                columns: table => new
                {
                    ForeignKeySchemaId = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: false
                    ),
                    ColumnName = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: true
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ForeignKeyColumn",
                        x => new { x.ForeignKeySchemaId, x.ColumnName }
                    );
                    table.ForeignKey(
                        name: "FK_ForeignKeyColumn_ForeignKeySchema_ForeignKeySchemaId",
                        column: x => x.ForeignKeySchemaId,
                        principalTable: "ForeignKeySchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ForeignKeyPrincipalColumn",
                columns: table => new
                {
                    ForeignKeySchemaId = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: false
                    ),
                    PrincipalColumnName = table.Column<string>(
                        type: "varchar(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(
                        type: "rowversion",
                        rowVersion: true,
                        nullable: true
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ForeignKeyPrincipalColumn",
                        x => new { x.ForeignKeySchemaId, x.PrincipalColumnName }
                    );
                    table.ForeignKey(
                        name: "FK_ForeignKeyPrincipalColumn_ForeignKeySchema_ForeignKeySchemaId",
                        column: x => x.ForeignKeySchemaId,
                        principalTable: "ForeignKeySchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ColumnSchema_TableSchemaId",
                table: "ColumnSchema",
                column: "TableSchemaId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ForeignKeySchema_TableSchemaId",
                table: "ForeignKeySchema",
                column: "TableSchemaId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_TableSchema_EtlPipelineId",
                table: "TableSchema",
                column: "EtlPipelineId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ColumnSchema");

            migrationBuilder.DropTable(name: "ForeignKeyColumn");

            migrationBuilder.DropTable(name: "ForeignKeyPrincipalColumn");

            migrationBuilder.DropTable(name: "ForeignKeySchema");

            migrationBuilder.DropTable(name: "TableSchema");
        }
    }
}
