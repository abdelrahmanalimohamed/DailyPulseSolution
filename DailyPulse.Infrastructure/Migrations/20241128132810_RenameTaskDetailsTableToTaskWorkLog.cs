using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class RenameTaskDetailsTableToTaskWorkLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDetails",
                table: "TaskDetails");

            migrationBuilder.RenameTable(
                name: "TaskDetails",
                newName: "TaskWorkLogs");

            migrationBuilder.RenameIndex(
                name: "IX_TaskDetails_TaskId",
                table: "TaskWorkLogs",
                newName: "IX_TaskWorkLogs_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskWorkLogs",
                table: "TaskWorkLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskWorkLogs_Tasks_TaskId",
                table: "TaskWorkLogs",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskWorkLogs_Tasks_TaskId",
                table: "TaskWorkLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskWorkLogs",
                table: "TaskWorkLogs");

            migrationBuilder.RenameTable(
                name: "TaskWorkLogs",
                newName: "TaskDetails");

            migrationBuilder.RenameIndex(
                name: "IX_TaskWorkLogs_TaskId",
                table: "TaskDetails",
                newName: "IX_TaskDetails_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDetails",
                table: "TaskDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
