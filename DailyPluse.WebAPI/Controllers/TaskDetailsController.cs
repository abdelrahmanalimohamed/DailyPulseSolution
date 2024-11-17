using DailyPulse.Application.CQRS.Commands.TaskDetails;
using DailyPulse.Application.CQRS.Queries.TaskDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskDetailsController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("createtaskdetails")]
        public async Task<IActionResult> CreateTaskDetails([FromBody] CreateTaskDetailsCommand createTaskDetailsCommand)
        {
            await _mediator.Send(createTaskDetailsCommand);
            return StatusCode(201);
        }

        [HttpPost("taskcompletion")]
        public async Task<IActionResult> CreateTaskCompletionDetails([FromBody] CreateTaskCompletionDetailsCommand createTaskCompletionDetailsCommand)
        {
            await _mediator.Send(createTaskCompletionDetailsCommand);
            return StatusCode(201);
        }

        [HttpGet("gettaskdetails")]
        public async Task<IActionResult> GetTaskDetails(Guid taskId)
        {
            var getTaskDetailsQuery = new GetTaskDetailsQuery { TaskId = taskId};

            var taskDetails = await _mediator.Send(getTaskDetailsQuery);

            return Ok(taskDetails);
        }
    }
}