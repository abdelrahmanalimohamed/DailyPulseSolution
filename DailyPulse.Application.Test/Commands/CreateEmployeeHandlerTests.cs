using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.CommandHandler.EmployeesHandlers;
using DailyPulse.Application.CQRS.Commands.Employees;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.Test.Commands
{
	public class CreateEmployeeHandlerTests
	{
	//	private Mock<IGenericRepository<Employee>> _mock;
	//	private CreateEmployeeHandler _createEmployeeHandler;

	//	public CreateEmployeeHandlerTests()
	//	{
	//		_mock = new Mock<IGenericRepository<Employee>>();
	//		_createEmployeeHandler = new CreateEmployeeHandler(_mock.Object);
	//	}

	//	[Fact]
	//	public async Task Handle_Create_New_Employee()
	//	{
	//		// Arrange
	//		var command = new CreateEmployeeCommand
	//		{
	//			Name = "Test",
	//			Email = "test@gmail.com",
	//			Jobgrade = "5", // Ensure this matches the expected role parsing (Trainee)
	//			ReportTo = Guid.NewGuid(),
	//			Password = "123456789", // Plain password to be hashed
	//			Title = "TestTitle"
	//		};

	//		// Act
	//		await _createEmployeeHandler.Handle(command, CancellationToken.None);

	//		// Assert
	//		_mock.Verify(repo => repo.AddAsync(It.Is<Employee>(
	//			emp => emp.Name == command.Name &&
	//					emp.Role == EmployeeRole.Trainee && 
	//					emp.Email == command.Email &&
	//					emp.ReportToId == command.ReportTo
	//		), It.IsAny<CancellationToken>()), Times.Once);

	//		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
	//		Assert.True(BCrypt.Net.BCrypt.Verify(command.Password, hashedPassword));
	//	}

	//	[Fact]
	//	public async Task Handle_Should_Throw_ArgumentException_When_Invalid_JobGrade_Is_Provided()
	//	{
	//		// Arrange
	//		var command = new CreateEmployeeCommand
	//		{
	//			Name = "John Doe",
	//			Title = "Software Engineer",
	//			Email = "john.doe@example.com",
	//			Password = "123456789",
	//			Jobgrade = "InvalidGrade", // Invalid grade
	//			ReportTo = Guid.NewGuid()
	//		};

	//		// Act & Assert
	//		await Assert.ThrowsAsync<ArgumentException>(() => _createEmployeeHandler.Handle(command, CancellationToken.None));
	//	}

	}
}