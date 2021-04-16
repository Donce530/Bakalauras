using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ReservationRealTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "RealEnd",
                table: "Reservations",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RealStart",
                table: "Reservations",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealEnd",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RealStart",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Reservations");
        }
    }
}
