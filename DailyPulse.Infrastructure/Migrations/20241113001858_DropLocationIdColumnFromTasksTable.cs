using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropLocationIdColumnFromTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Locations_LocationId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_LocationId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LocationId",
                table: "Tasks",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Locations_LocationId",
                table: "Tasks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
