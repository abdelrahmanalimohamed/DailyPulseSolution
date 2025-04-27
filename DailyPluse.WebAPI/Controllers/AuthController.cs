using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Application.CQRS.Commands.ForgetPassword;
using DailyPulse.Application.CQRS.Commands.ResetPassword;
using DailyPulse.Application.CQRS.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("loginemployee")]
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

	[HttpPost("forgetpassword")]
	public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand forgetPasswordCommand)
    {
        var forgetPassword = await mediator.Send(forgetPasswordCommand);
        
        return Ok(forgetPassword);
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
    {
        var resetPassword =  await mediator.Send(resetPasswordCommand);
        return Ok(resetPassword);
    }
}