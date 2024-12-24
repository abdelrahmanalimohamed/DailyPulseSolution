using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Application.CQRS.Queries.Projects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
        [Authorize(Policy = "AdminRoles")]
        [HttpGet]
        public async Task<IActionResult> GetAllProjects() 
        {
            var query = new GetProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

		[Authorize(Policy = "SeniorRoles")]
		[HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var query = new GetProjectByIdQuery{ ProjectId = id };
            var project = await _mediator.Send(query);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectCommand createProjectCommand)
        {
            await _mediator.Send(createProjectCommand);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id , [FromBody] UpdateProjectCommand updateProjectCommand)
        {
            if (id != updateProjectCommand.ProjectId)
                return BadRequest("Project ID mismatch");

            await _mediator.Send(updateProjectCommand);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var command = new DeleteProjectCommand { ProjectId = id };
            await _mediator.Send(command);
            return Ok();
        }
    } 
}