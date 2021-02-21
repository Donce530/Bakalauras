using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class OpenHoursIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OpenHours_WeekDay_RestaurantId",
                table: "OpenHours",
                columns: new[] { "WeekDay", "RestaurantId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenHours_WeekDay_RestaurantId",
                table: "OpenHours");
        }
    }
}
