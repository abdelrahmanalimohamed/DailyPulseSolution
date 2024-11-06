﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        private readonly IGenericRepository<Employee> _repository;

        public CreateEmployeeHandler(IGenericRepository<Employee> _repository)
        {
            this._repository = _repository;
        }
        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var employee = new Employee
            {
                Name = request.Name,
                Title = request.Title,
                username = request.Email,
                password = hashedPassword,
                Role = request.Role,
                ReportToId = request.ReportTo,
                IsAdmin = request.Role == EmployeeRole.Admin 
            };

            await _repository.AddAsync(employee, cancellationToken);
            return Unit.Value;
        }
    }
}
