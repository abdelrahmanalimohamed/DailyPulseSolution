using DailyPulse.Application.CQRS.Commands.Reassign;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReAssignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReAssignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createreassign")]
        public async Task<IActionResult> ReAssignation([FromBody] CreateReAssignationCommand createReAssignationCommand)
        {
            await _mediator.Send(createReAssignationCommand);
            return StatusCode(201);
        }
    }
}
