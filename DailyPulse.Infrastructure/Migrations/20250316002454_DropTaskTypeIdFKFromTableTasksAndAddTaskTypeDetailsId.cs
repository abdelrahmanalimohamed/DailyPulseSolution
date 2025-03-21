using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropTaskTypeIdFKFromTableTasksAndAddTaskTypeDetailsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskType_TaskTypeId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskTypeId",
                table: "Tasks",
                newName: "TaskTypeDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TaskTypeId",
                table: "Tasks",
                newName: "IX_Tasks_TaskTypeDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskTypeDetails_TaskTypeDetailsId",
                table: "Tasks",
                column: "TaskTypeDetailsId",
                principalTable: "TaskTypeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskTypeDetails_TaskTypeDetailsId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskTypeDetailsId",
                table: "Tasks",
                newName: "TaskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TaskTypeDetailsId",
                table: "Tasks",
                newName: "IX_Tasks_TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskType_TaskTypeId",
                table: "Tasks",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
