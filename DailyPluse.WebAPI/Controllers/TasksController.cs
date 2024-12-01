using DailyPulse.Application.CQRS.Commands.Tasks;
using DailyPulse.Application.CQRS.Queries.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand createTaskCommand)
        {
            await _mediator.Send(createTaskCommand);

            return StatusCode(201);
        }

        [HttpPost("reject-rejectTask")]
        public async Task<IActionResult> RejectTaskByEmployee([FromBody] UpdateTaskRejectionByEmployeeCommand updateTaskRejectionByEmployeeCommand)
        {
            await _mediator.Send(updateTaskRejectionByEmployeeCommand);
            return StatusCode(201);
        }

        [HttpPut("updatetaskstatus")]
        public async Task<IActionResult> UpdateTaskStatusByAdmin([FromBody] UpdateTaskStatusByAdminCommand updateTaskStatusByAdminCommand)
        {
            await _mediator.Send(updateTaskStatusByAdminCommand);
            return StatusCode(201);
        }

        [HttpPut("updatetaskstatusbyemployee")]
        public async Task<IActionResult> UpdateTaskStatusByEmployee([FromBody] UpdateTaskStatusByEmployeeCommand updateTaskStatusByEmployeeCommand)
        {
            await _mediator.Send(updateTaskStatusByEmployeeCommand);
            return StatusCode(201);
        }

        [HttpPut("update-updatetask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            await _mediator.Send(updateTaskCommand);
            return StatusCode(200);
        }

        [HttpPut("closeorcompletetask")]
        public async Task<IActionResult> CloseOrCompleteTaskByAdmin([FromBody] CloseTaskCommand closeTaskCommand)
        {
            await _mediator.Send(closeTaskCommand);
            return StatusCode(201);
        }

        [HttpGet("getalltasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var getTasksQuery = new GetTasksQuery();
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

        [HttpGet("gettaskreport")]
        public async Task<IActionResult> GetTaskReport(Guid taskId)
        {
            var getTaskReportQuery = new GetTaskReportQuery { TaskId = taskId };

            var report = await _mediator.Send(getTaskReportQuery);

            return Ok(report);
        }
    }
}