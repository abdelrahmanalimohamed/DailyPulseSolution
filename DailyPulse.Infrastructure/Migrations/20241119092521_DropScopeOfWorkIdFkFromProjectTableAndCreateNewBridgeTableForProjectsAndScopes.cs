using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class DropScopeOfWorkIdFkFromProjectTableAndCreateNewBridgeTableForProjectsAndScopes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ScopeOfWorks_ScopeOfWorkId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ScopeOfWorkId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ScopeOfWorkId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectsScopes",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ScopeOfWorkId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsScopes", x => new { x.ProjectId, x.ScopeOfWorkId });
                    table.ForeignKey(
                        name: "FK_ProjectsScopes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsScopes_ScopeOfWorks_ScopeOfWorkId",
                        column: x => x.ScopeOfWorkId,
                        principalTable: "ScopeOfWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsScopes_ScopeOfWorkId",
                table: "ProjectsScopes",
                column: "ScopeOfWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectsScopes");

            migrationBuilder.AddColumn<Guid>(
                name: "ScopeOfWorkId",
                table: "Projects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ScopeOfWorkId",
                table: "Projects",
                column: "ScopeOfWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ScopeOfWorks_ScopeOfWorkId",
                table: "Projects",
                column: "ScopeOfWorkId",
                principalTable: "ScopeOfWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
