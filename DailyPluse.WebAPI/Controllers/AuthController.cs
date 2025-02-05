using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Application.CQRS.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("loginemployee")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery) 
        { 
            var loginResponse = await mediator.Send(loginQuery);

            if (loginResponse.IsSuccess == false)
                return Unauthorized();

            return Ok(loginResponse);
        }

        [HttpPost("verifyuser")]
        public async Task<IActionResult> VerifyUser([FromBody] VerifyEmployeeEmailCommand verifyEmployeeEmailCommand)
        {
            var verifyResponse = await mediator.Send(verifyEmployeeEmailCommand);
            return Ok(verifyResponse);
        }
    }
}