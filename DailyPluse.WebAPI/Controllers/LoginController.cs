using DailyPulse.Application.CQRS.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator mediator;
        public LoginController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("loginemployee")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery) 
        { 
            var loginResponse = await mediator.Send(loginQuery);
            return Ok(loginResponse);
        }
    }
}
