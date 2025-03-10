using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddMachineNameInStatusLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "TaskStatusLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByMachine",
                table: "Tasks",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "TaskStatusLogs");

            migrationBuilder.DropColumn(
                name: "CreatedByMachine",
                table: "Tasks");
        }
    }
}
