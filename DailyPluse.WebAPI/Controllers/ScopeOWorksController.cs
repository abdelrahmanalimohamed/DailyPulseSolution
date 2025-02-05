using DailyPulse.Application.CQRS.Queries.ScopeOfWorks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	//[Authorize]
	public class ScopeOWorksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScopeOWorksController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetScopeOfWorks()
        {
            var query = new GetScopeOfWorksQuery();
            var scopes = await _mediator.Send(query);
            return Ok(scopes);
        }
    }
}
