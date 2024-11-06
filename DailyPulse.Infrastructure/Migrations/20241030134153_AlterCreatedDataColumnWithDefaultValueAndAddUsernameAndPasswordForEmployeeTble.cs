using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AlterCreatedDataColumnWithDefaultValueAndAddUsernameAndPasswordForEmployeeTble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tasks",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ScopeOfWorks",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Regions",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ReAssigns",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Locations",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURDATE()");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tasks",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskDetails",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ScopeOfWorks",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Regions",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ReAssigns",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Locations",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "current_timestamp()");
        }
    }
}
