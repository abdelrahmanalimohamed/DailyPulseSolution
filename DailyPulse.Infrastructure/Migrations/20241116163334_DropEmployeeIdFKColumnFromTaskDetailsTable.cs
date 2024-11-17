using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropEmployeeIdFKColumnFromTaskDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_Employees_EmployeeId",
                table: "TaskDetails");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_EmployeeId",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TaskDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "TaskDetails",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_EmployeeId",
                table: "TaskDetails",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_Employees_EmployeeId",
                table: "TaskDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
