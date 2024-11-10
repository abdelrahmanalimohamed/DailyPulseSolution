using DailyPulse.Application.CQRS.Commands.Tasks;
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
    }
}
