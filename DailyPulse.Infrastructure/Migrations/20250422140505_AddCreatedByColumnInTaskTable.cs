using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddCreatedByColumnInTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskCreatedBy",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskCreatedBy",
                table: "Tasks",
                column: "TaskCreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_TaskCreatedBy",
                table: "Tasks",
                column: "TaskCreatedBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_TaskCreatedBy",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskCreatedBy",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskCreatedBy",
                table: "Tasks");
        }
    }
}
