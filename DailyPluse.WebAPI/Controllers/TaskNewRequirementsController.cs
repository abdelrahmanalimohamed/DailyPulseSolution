using DailyPulse.Application.CQRS.Commands.TaskNewRequirements;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskNewRequirementsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TaskNewRequirementsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("addnewrequirements")]
        public async Task<IActionResult> CreateNewRequirements([FromBody] CreateTaskNewRequirementsCommand createTaskNewRequirementsCommand)
        {
            await mediator.Send(createTaskNewRequirementsCommand);
            return StatusCode(201);
        }
    }
}
