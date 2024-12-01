using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class CreateNewStoredProcedureToGetTaskReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
           CREATE PROCEDURE GetTaskReport(IN TaskId CHAR(36))
                        BEGIN
                   SELECT
                    ts.Name,
                    PR.Description AS ProjectName,
                    ts.EstimatedWorkingHours,
	
                  CONCAT(
                        FLOOR(SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY ts.Id) / 3600), ' hours ',
                        FLOOR((SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY ts.Id) % 3600) / 60), ' minutes ',
                        SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY ts.Id) % 60, ' seconds'
                    ) AS Total_Working_Hours,

                    CASE
                        WHEN TIMESTAMPDIFF(HOUR, tswl.StartTime, tswl.PauseTime) != 0 THEN 
                            CONCAT(TIMESTAMPDIFF(HOUR, tswl.StartTime, tswl.PauseTime), ' hours')
                        WHEN TIMESTAMPDIFF(MINUTE, tswl.StartTime, tswl.PauseTime) != 0 THEN 
                            CONCAT(TIMESTAMPDIFF(MINUTE, tswl.StartTime, tswl.PauseTime), ' minutes')
                        ELSE 
                            CONCAT(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime), ' seconds')
                    END AS Time_Difference,
    
                    tswl.LogDesc , 
                    tswl.StartTime , 
                    tswl.PauseTime ,
                    tswl.EndTime ,
                    tsl.OldStatus AS OldValue,
                    tsl.NewStatus AS NewValue

                FROM
                    tasks AS ts
                LEFT JOIN tasknewrequirements AS tsr ON ts.Id = tsr.TaskId
                LEFT JOIN reassigns AS r ON ts.Id = r.TaskId
                LEFT JOIN tasklogs AS tl ON ts.Id = tl.TaskId
                LEFT JOIN projects AS PR ON ts.ProjectId = PR.Id
                LEFT JOIN taskworklogs AS tswl ON ts.Id = tswl.TaskId
                LEFT JOIN rejectedtasks AS tr ON ts.Id = tr.TaskId
                LEFT JOIN taskstatuslogs AS tsl ON ts.Id = tsl.TaskId
                WHERE ts.Id = TaskId;
                  END
               ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTaskReport");
        }
    }
}
