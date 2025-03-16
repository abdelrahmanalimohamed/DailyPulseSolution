using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddTaskTypeIdAsFKInTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskTypeId",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskTypeId",
                table: "Tasks",
                column: "TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskType_TaskTypeId",
                table: "Tasks",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskType_TaskTypeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskTypeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "Tasks");
        }
    }
}
