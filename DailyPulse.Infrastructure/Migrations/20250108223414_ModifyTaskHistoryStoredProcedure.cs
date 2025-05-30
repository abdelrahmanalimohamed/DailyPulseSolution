﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPulse.Infrastructure.Migrations
{
    public partial class ModifyTaskHistoryStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
DROP PROCEDURE IF EXISTS GetTaskHistory;
CREATE PROCEDURE GetTaskHistory (IN TaskId CHAR(36))
             BEGIN
        SELECT 
     DATE_FORMAT(t.CreatedDate, '%d- %M-%Y   %H:%i') as 'DateAndTime' ,
    t.OldStatus, 
    t.NewStatus, 
    CASE 
        WHEN t.OldStatus = 1 AND t.NewStatus = 6 THEN r.RejectionReasons
        ELSE NULL
    END AS 'BackToAdminReasons',
    CASE 
        WHEN t.OldStatus = 1 AND t.NewStatus = 6 THEN DATE_FORMAT(r.CreatedDate, '%d- %M-%Y   %H:%i') 
        ELSE NULL
    END AS 'BackToAdminTime',
    CASE 
       WHEN t.OldStatus = 4 AND t.NewStatus = 7 THEN DATE_FORMAT( t2.CreatedDate , '%d- %M-%Y   %H:%i')
       ELSE NULL 
       END AS 'AdminClosingTime' , 
t2.ClosedComments AS 'AdminClosingReasons'
FROM 
    taskstatuslogs t 
LEFT JOIN 
    tasks t1 ON t.TaskId = t1.Id
LEFT JOIN 
    employeerejectedtasks r ON t1.Id = r.TaskId
LEFT JOIN 
    adminrejectedtasks  t2 ON t1.Id = t2.TaskId

     WHERE t.TaskId = TaskId
ORDER BY 
    t.CreatedDate;
       END
    ");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTaskHistory");
		}
    }
}
