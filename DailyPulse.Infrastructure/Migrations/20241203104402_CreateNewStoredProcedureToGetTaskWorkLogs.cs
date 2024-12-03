using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class CreateNewStoredProcedureToGetTaskWorkLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE GetTaskWorkLogs (IN TaskId CHAR(36))
             BEGIN
        SELECT 
   ROW_NUMBER() OVER (PARTITION BY tswl.TaskId ORDER BY tswl.StartTime) AS TaskWorkLogID ,
   ts.Name ,
   DATE_FORMAT(tswl.StartTime, '%Y-%m-%d %H:%i:%s') AS Start_DateTime,
    DATE_FORMAT(tswl.PauseTime, '%Y-%m-%d %H:%i:%s') AS Pause_DateTime,
CONCAT(
      FLOOR(SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY tswl.TaskId) / 3600), ' hours ',
      FLOOR((SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY tswl.TaskId) % 3600) / 60), ' minutes ',
      SUM(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime)) OVER (PARTITION BY tswl.TaskId) % 60, ' seconds'
  ) AS Total_Working_Hours, 
  tswl.LogDesc ,
    CASE
        WHEN TIMESTAMPDIFF(HOUR, tswl.StartTime, tswl.PauseTime) != 0 THEN 
            CONCAT(TIMESTAMPDIFF(HOUR, tswl.StartTime, tswl.PauseTime), ' hours')
        
        WHEN TIMESTAMPDIFF(MINUTE, tswl.StartTime, tswl.PauseTime) != 0 THEN 
            CONCAT(TIMESTAMPDIFF(MINUTE, tswl.StartTime, tswl.PauseTime), ' minutes')
   
        ELSE 
            CONCAT(TIMESTAMPDIFF(SECOND, tswl.StartTime, tswl.PauseTime), ' seconds')
    END AS  WorkLogDuration
FROM taskworklogs AS tswl
join tasks as ts on tswl.TaskId = ts.Id 
     WHERE ts.Id = TaskId;
       END
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTaskWorkLogs");
        }
    }
}