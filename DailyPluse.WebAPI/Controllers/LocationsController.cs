using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator , ApplicationDbContext applicationDbContext)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var query = new GetLocationsQuery();
            var locations = await _mediator.Send(query);

            var x = locations.Select(a => new { a.Id, a.Name });

            return Ok(x);
        }

        [HttpGet("by-locationid/{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var query = new GetLocationByIdQuery { LocationId = id };
            var location = await _mediator.Send(query);
            return Ok(location);
        }

        [HttpGet("by-regionid/{regionId}")]
        public async Task<IActionResult> GetLocationByRegionId(Guid regionId)
        {
            var query = new GetLocationsByRegionIdQuery { RegionId = regionId };
            var locations = await _mediator.Send(query);

            var selectedLocations = locations.Select(location => new
            {
                location.Id,
                location.Name
            });

            return Ok(selectedLocations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
        {
            await _mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(Guid id , [FromBody] UpdateLocationCommand command)
        {
            if (id != command.LocationId)
                return BadRequest("Location ID mismatch");


            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var command = new DeleteLocationCommand { LocationId = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
