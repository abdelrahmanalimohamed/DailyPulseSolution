using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class AddNewTableWithNameProfitCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfitCenterId",
                table: "Projects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ProfitCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProfitCenterNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfitCenterDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitCenters", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProfitCenterId",
                table: "Projects",
                column: "ProfitCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfitCenters_ProfitCenterNumber",
                table: "ProfitCenters",
                column: "ProfitCenterNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProfitCenters_ProfitCenterId",
                table: "Projects",
                column: "ProfitCenterId",
                principalTable: "ProfitCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProfitCenters_ProfitCenterId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProfitCenters");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProfitCenterId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProfitCenterId",
                table: "Projects");
        }
    }
}
