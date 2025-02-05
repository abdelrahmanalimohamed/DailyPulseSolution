using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class RenameTablesRejectedTaskAndTaskLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RejectedTasks");

            migrationBuilder.DropTable(
                name: "TaskLogs");

            migrationBuilder.CreateTable(
                name: "AdminRejectedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClosedComments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRejectedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminRejectedTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeRejectedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmpId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RejectionReasons = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRejectedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRejectedTasks_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRejectedTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRejectedTasks_TaskId",
                table: "AdminRejectedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRejectedTasks_EmpId",
                table: "EmployeeRejectedTasks",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRejectedTasks_TaskId",
                table: "EmployeeRejectedTasks",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRejectedTasks");

            migrationBuilder.DropTable(
                name: "EmployeeRejectedTasks");

            migrationBuilder.CreateTable(
                name: "RejectedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmpId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "current_timestamp()"),
                    RejectionReasons = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectedTasks_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RejectedTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NewAssignedEmp = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    OldAssignedEmp = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    TaskId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClosedComments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "current_timestamp()"),
                    NewRequirements = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RejectReasons = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskLogs_Employees_NewAssignedEmp",
                        column: x => x.NewAssignedEmp,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskLogs_Employees_OldAssignedEmp",
                        column: x => x.OldAssignedEmp,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskLogs_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedTasks_EmpId",
                table: "RejectedTasks",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedTasks_TaskId",
                table: "RejectedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogs_NewAssignedEmp",
                table: "TaskLogs",
                column: "NewAssignedEmp");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogs_OldAssignedEmp",
                table: "TaskLogs",
                column: "OldAssignedEmp");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogs_TaskId",
                table: "TaskLogs",
                column: "TaskId");
        }
    }
}
