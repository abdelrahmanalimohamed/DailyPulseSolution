using DailyPulse.Application.CQRS.Queries.Regions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class RegionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegionsController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var query = new GetRegionsQuery();
            var regions = await _mediator.Send(query);
            return Ok(regions);
        }
    }
}
