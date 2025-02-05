using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddEmailVerifiedFlagToEmployeeTableAndAdjustTheColumnsNameWithCapitalLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Employees",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Employees",
                newName: "Password");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Employees",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Employees",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employees",
                newName: "password");
        }
    }
}
