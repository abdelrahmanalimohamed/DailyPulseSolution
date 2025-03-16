using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class CreateTaskTypeDetailsTableAndAddTaskTypeIdAsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskTypeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTypeDetails_TaskType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypeDetails_TaskTypeId",
                table: "TaskTypeDetails",
                column: "TaskTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTypeDetails");
        }
    }
}
