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
        public async Task<IActionResult> CreateTask(CreateTaskCommand createTaskCommand)
        {
            await _mediator.Send(createTaskCommand);

            return StatusCode(201);
        }

        [HttpGet("getbyTasksEmployeeId")]
        public async Task<IActionResult> GetTasksByEmployeeId(Guid empId)
        {
            var getTasksByEmployeeIdQuery = new GetTasksByEmployeeIdQuery { EmployeeId = empId };
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
    }
}
