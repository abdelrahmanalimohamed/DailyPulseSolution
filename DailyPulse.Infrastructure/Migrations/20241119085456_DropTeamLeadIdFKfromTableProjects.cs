using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropTeamLeadIdFKfromTableProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_TeamLeadId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TeamLeadId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeamLeadId",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamLeadId",
                table: "Projects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamLeadId",
                table: "Projects",
                column: "TeamLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_TeamLeadId",
                table: "Projects",
                column: "TeamLeadId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
