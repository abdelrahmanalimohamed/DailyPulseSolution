using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Application.CQRS.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyPluse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand employeeCommand)
        {
            await _mediator.Send(employeeCommand);
            return StatusCode(201);
        }

        [HttpGet("getemployess")]
        public async Task<IActionResult> GetEmployees()
        {
            var employeeGetQuery = new GetEmployeesQuery();
            var employess = await _mediator.Send(employeeGetQuery);
            return Ok(employess);
        }
    }
}
