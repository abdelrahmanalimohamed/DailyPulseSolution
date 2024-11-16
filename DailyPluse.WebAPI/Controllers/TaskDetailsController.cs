using DailyPulse.Application.CQRS.Commands.Tasks;
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
    }
}