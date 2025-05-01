using DailyPulse.Application.CQRS.Commands.TaskNewRequirements;
using DailyPulse.Application.CQRS.Queries.TaskNewRequirements;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet("getTaskNewRequirements")]
    public async Task<IActionResult> GetTaskNewRequirements(Guid taskId)
    {
        var getTaskNewRequirementsQuery = new GetTaskNewRequirementsQuery { TaskId = taskId };
        var results = await mediator.Send(getTaskNewRequirementsQuery);
        return Ok(results);
    }
}