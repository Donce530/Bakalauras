using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class TableLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableLinks",
                columns: table => new
                {
                    FirstTableId = table.Column<int>(type: "integer", nullable: false),
                    SecondTableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableLinks", x => new { x.FirstTableId, x.SecondTableId });
                    table.ForeignKey(
                        name: "FK_TableLinks_PlanTables_FirstTableId",
                        column: x => x.FirstTableId,
                        principalTable: "PlanTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableLinks_PlanTables_SecondTableId",
                        column: x => x.SecondTableId,
                        principalTable: "PlanTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableLinks_SecondTableId",
                table: "TableLinks",
                column: "SecondTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableLinks");
        }
    }
}
