using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropScopeOfWorkColumnFromTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ScopeOfWorks_ScopeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ScopeId",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ScopeId",
                table: "Tasks",
                column: "ScopeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ScopeOfWorks_ScopeId",
                table: "Tasks",
                column: "ScopeId",
                principalTable: "ScopeOfWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
