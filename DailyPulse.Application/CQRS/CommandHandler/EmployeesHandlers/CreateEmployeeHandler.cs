using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Application.DTO;
using DailyPulse.Application.Extensions;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using MediatR;
using System.Data;
using System.Text.RegularExpressions;

namespace DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers
{
	public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand , CreateEmployeeResponseDTO>
    {
        private readonly IGenericRepository<Employee> _repository;
		private readonly IEmailServices _emailService;
		private readonly IEmailTemplateService _emailTemplateService;
		public CreateEmployeeHandler(
            IGenericRepository<Employee> _repository ,
			IEmailServices _emailService ,
			IEmailTemplateService _emailTemplateService)
        {
            this._repository = _repository;
            this._emailService = _emailService;
            this._emailTemplateService = _emailTemplateService;
        }
        public async Task<CreateEmployeeResponseDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await CheckEmployeeByName(request.Name , request.Email , cancellationToken);

			if (existingEmployee != null)
			{
				throw new DuplicateNameException("An Employee with the same name or email already exists.");
			}

			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            string grade = Regex.Replace(request.Jobgrade, @"\s+", "");

            var employee = new Employee
            {
                Name = request.Name,
                Title = request.Title,
                Email = request.Email,
                Password = hashedPassword,
                Role = Enum.TryParse(grade, true, out EmployeeRole role)
                     ? role : throw new ArgumentException($"Invalid job grade: {request.Jobgrade}"),
                ReportToId = request.ReportTo,
                IsAdmin = false 
            };

            await _repository.AddAsync(employee, cancellationToken);

			var verificationLink = $"http://192.68.121.17:5000/verify-email?token={employee.Id}";

			var emailSubject = _emailTemplateService.GetVerificationEmailSubject();

			var emailBody =  _emailTemplateService.GenerateVerificationEmailBodyAsync(verificationLink);

			 _emailService.SendEmailAsync(employee.Email, emailSubject, emailBody);

			return new CreateEmployeeResponseDTO
			{
				EmployeeId = employee.Id,
				Email = employee.Email
			};
		}
        private async Task<Employee> CheckEmployeeByName(string requestName , string email , CancellationToken cancellationToken)
        {
			var normalizedName = requestName.RemoveWhitespace();
			var normalizedEmail = email.RemoveWhitespace();

			var existingEmployee = await _repository.GetFirstOrDefault(
						emp => emp.Name.Trim().ToLower() == normalizedName.ToLower() 
						|| emp.Email.Trim().ToLower() == normalizedEmail.ToLower(),
						cancellationToken);

            return existingEmployee;
		}
	}
}