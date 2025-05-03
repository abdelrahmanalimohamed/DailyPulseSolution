using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Application.CQRS.Queries.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
		private readonly string _machineName;
		public TasksController(IMediator _mediator, IHttpContextAccessor httpContextAccessor)
		{
			this._mediator = _mediator;
			_machineName = ResolveClientMachineName(httpContextAccessor);
		}

		[HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand createTaskCommand)
        {
			createTaskCommand.MachineName = _machineName;

			await _mediator.Send(createTaskCommand);
            return StatusCode(201);
        }

        [HttpPost("reject-rejectTask")]
        public async Task<IActionResult> RejectTaskByEmployee([FromBody] UpdateTaskRejectionByEmployeeCommand updateTaskRejectionByEmployeeCommand)
        {
			updateTaskRejectionByEmployeeCommand.MachineName = _machineName;

			await _mediator.Send(updateTaskRejectionByEmployeeCommand);
            return StatusCode(201);
        }

        [HttpPut("updatetaskstatus")]
        public async Task<IActionResult> UpdateTaskStatusByAdmin([FromBody] UpdateTaskStatusByAdminCommand updateTaskStatusByAdminCommand)
        {
			updateTaskStatusByAdminCommand.MachineName = _machineName;

			await _mediator.Send(updateTaskStatusByAdminCommand);
            return StatusCode(201);
        }

        [HttpPut("updatetaskstatusbyemployee")]
        public async Task<IActionResult> UpdateTaskStatusByEmployee([FromBody] UpdateTaskStatusByEmployeeCommand updateTaskStatusByEmployeeCommand)
        {
			updateTaskStatusByEmployeeCommand.MachineName = _machineName;

			await _mediator.Send(updateTaskStatusByEmployeeCommand);
            return StatusCode(201);
        }

        [HttpPut("update-updatetask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand updateTaskCommand)
        {
			updateTaskCommand.MachineName = _machineName;

			await _mediator.Send(updateTaskCommand);
            return StatusCode(200);
        }

        [HttpPut("closeorcompletetask")]
        public async Task<IActionResult> CloseOrCompleteTaskByAdmin([FromBody] CloseTaskCommand closeTaskCommand)
        {
			closeTaskCommand.MachineName = _machineName;

			await _mediator.Send(closeTaskCommand);
            return StatusCode(201);
        }

        [HttpGet("getalltasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var getTasksQuery = new GetTasksQuery();
            var x = Environment.MachineName;
			var tasks = await _mediator.Send(getTasksQuery);
            return Ok(tasks);
        }

        [HttpGet("getbyTasksEmployeeId")]
        public async Task<IActionResult> GetTasksByEmployeeId(Guid empId)
        {
            var getTasksByEmployeeIdQuery = new GetAllTasksByEmployeeIdQuery { EmployeeId = empId };
            var tasks = await _mediator.Send(getTasksByEmployeeIdQuery);
            return Ok(tasks);
        }

        [HttpGet("getbyTaskId")]
        public async Task<IActionResult> GetTasksById(Guid taskId)
        {
            var getTaskByIdQuery = new GetTaskByIdQuery { TaskId = taskId };
            var tasks = await _mediator.Send(getTaskByIdQuery);
            return Ok(tasks);
        }

        [HttpGet("getworkedtasks")]
        public async Task<IActionResult> GetWorkedTasks(Guid empId)
        {
            var getWorkedTasksByEmployeeIdQuery = new GetWorkedTasksByEmployeeIdQuery { EmployeeId = empId };
            var workedTasks = await _mediator.Send(getWorkedTasksByEmployeeIdQuery);
            return Ok(workedTasks);
        }

        [HttpGet("gettaskworklog")]
        public async Task<IActionResult> GetTaskWorkLog(Guid taskId)
        {
            var getTaskReportQuery = new GetTaskWorkLogQuery { TaskId = taskId };

            var report = await _mediator.Send(getTaskReportQuery);

            return Ok(report);
        }

        [HttpGet("gettaskhistory")]
        public async Task<IActionResult> GetTaskHistory(Guid taskId)
        {
            var getTaskHistoryQuery = new GetTaskHistoryQuery { TaskId = taskId };

            var report = await _mediator.Send(getTaskHistoryQuery);

            return Ok(report);
        }

        [HttpGet("getTaskTypes")]
        public async Task<IActionResult> GetTasksTypes()
        {
            var getTaskTypesQuery = new GetTaskTypesQuery();
            var taskstypes = await _mediator.Send(getTaskTypesQuery);
            return Ok(taskstypes);
		}

        [HttpGet("getTaskTypesDetails")]
        public async Task<IActionResult> GetTaskTypesDetails(Guid tasktypeId)
        {
            var getTaskTypesDetails = new GetTaskTypeDetailsQuery { tasktypeId = tasktypeId };
            var taskdetailsTypes = await _mediator.Send(getTaskTypesDetails);
            return Ok(taskdetailsTypes);
        }

        [HttpGet("getTaskInnerDetails")]
        public async Task<IActionResult> GetTaskInnerDetails(Guid taskId)
        {
			var getTaskInnerDetailsQuery = new GetTaskInnerDetailsQuery { TaskId = taskId };
			var taskInnerDetails = await _mediator.Send(getTaskInnerDetailsQuery);
			return Ok(taskInnerDetails);
		}

        [HttpGet("getTaskClosedOrCompleted")]
        public async Task<IActionResult> GetTaskCompletedOrClosed(Guid EmployeeId)
        {
            GetTasksClosedOrCompletedByEmployeeIdQuery getTasksClosedOrCompletedByEmployeeId = new GetTasksClosedOrCompletedByEmployeeIdQuery { EmployeeID = EmployeeId };
            var closedOrCompletedTask = await _mediator.Send(getTasksClosedOrCompletedByEmployeeId);
            return Ok(closedOrCompletedTask);
		}
		private string ResolveClientMachineName(IHttpContextAccessor httpContextAccessor)
		{
			//var remoteIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
			//if (!string.IsNullOrEmpty(remoteIp))
			//{
			//	try
			//	{
			//		var hostEntry = Dns.GetHostEntry(remoteIp);
			//		return hostEntry.HostName;
			//	}
			//	catch
			//	{
			//		return remoteIp;
			//	}
			//}
			return Environment.MachineName;
		}
	}
}