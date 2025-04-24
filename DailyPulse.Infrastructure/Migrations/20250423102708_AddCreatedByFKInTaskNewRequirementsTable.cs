using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddCreatedByFKInTaskNewRequirementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TaskNewRequirements",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNewRequirements_CreatedBy",
                table: "TaskNewRequirements",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskNewRequirements_Employees_CreatedBy",
                table: "TaskNewRequirements",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskNewRequirements_Employees_CreatedBy",
                table: "TaskNewRequirements");

            migrationBuilder.DropIndex(
                name: "IX_TaskNewRequirements_CreatedBy",
                table: "TaskNewRequirements");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TaskNewRequirements");
        }
    }
}
