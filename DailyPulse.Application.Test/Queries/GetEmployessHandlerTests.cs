using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Employees;
using DailyPulse.Application.CQRS.QueriesHandler.EmployeeHandlers;
using DailyPulse.Domain.Entities;
using DailyPulse.Domain.Enums;
using Moq;
using System.Linq.Expressions;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.Test.Queries
{
	public class GetEmployessHandlerTests
	{
		private readonly Mock<IGenericRepository<Employee>> _mock;
		private readonly GetEmployeesHandler _getEmployeesHandler;
		private readonly GetEmployeeByIdHandler _getEmployeeByIdHandler;
		private readonly GetEmployeeSupervisorsHandler _getEmployeeSupervisorsHandler;
		private readonly GetEmployeeTeamLeaderHandler _getEmployeeTeamLeaderHandler;

		public GetEmployessHandlerTests()
		{
			_mock = new Mock<IGenericRepository<Employee>>();
			_getEmployeesHandler = new GetEmployeesHandler(_mock.Object);
			_getEmployeeByIdHandler = new GetEmployeeByIdHandler(_mock.Object);
			_getEmployeeSupervisorsHandler  = new GetEmployeeSupervisorsHandler(_mock.Object);
			_getEmployeeTeamLeaderHandler = new GetEmployeeTeamLeaderHandler(_mock.Object);
		}

		[Fact]
		public async Task Handle_Should_Return_List_Of_Employees()
		{
			// Arrange
			var employees = new List<Employee>
				{
					new Employee { Id = Guid.NewGuid(), Name = "Test1", IsAdmin = false },
					new Employee { Id = Guid.NewGuid(), Name = "Test2", IsAdmin = false }
				};

			_mock
				.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Employee, bool>> predicate, CancellationToken _) =>
					employees.AsQueryable().Where(predicate.Compile()).ToList());

			var query = new GetEmployeesQuery();
			var cancellationToken = CancellationToken.None;

			// Act
			var result = await _getEmployeesHandler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(employees.Count, result.Count());

			foreach (var employee in employees)
			{
				Assert.Contains(result, r => r.Id == employee.Id && r.Name == employee.Name);
			}

			// Verify that FindAsync was called once with the correct filter
			_mock.Verify(repo => repo.FindAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Employee, bool>>>(), cancellationToken), Times.Once);
		}

		[Fact]
		public async Task Handle_Should_Return_One_Employee()
		{
			// Arrange
			var employeeId = Guid.NewGuid();
			var employee = new Employee { Id = employeeId, Name = "Test1" };

			var query = new GetEmployeeByIdQuery { EmployeeId = employeeId };

			_mock
				 .Setup(repo => repo.GetByIdAsync(query.EmployeeId, It.IsAny<CancellationToken>()))
				 .ReturnsAsync(employee);

			// Act
			var result = await _getEmployeeByIdHandler.Handle(query, CancellationToken.None);


			// Assert
			Assert.NotNull(result);
			Assert.Equal(employee.Id, result.Id);
			Assert.Equal("Test1", result.Name);

			_mock.Verify(repo => repo.GetByIdAsync(employee.Id, It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_Should_Return_Supervisors_Employees()
		{
			// Arrange
			var employees = new List<Employee>
				{
					new Employee { Id = Guid.NewGuid(), Name = "Senior",  Role = EmployeeRole.Senior},
					new Employee { Id = Guid.NewGuid(), Name = "TeamLeader", Role = EmployeeRole.TeamLeader }
				};

			_mock
				.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Employee, bool>> predicate, CancellationToken _) =>
					employees.AsQueryable().Where(predicate.Compile()).ToList());

			var query = new GetEmployeeSupervisorsQuery();
			var cancellationToken = CancellationToken.None;

			// Act
			var result = await _getEmployeeSupervisorsHandler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(employees.Count, result.Count());

			foreach (var employee in employees)
			{
				Assert.Contains(result, r => r.Id == employee.Id && r.Name == employee.Name);
			}

			// Verify that FindAsync was called once with the correct filter
			_mock.Verify(repo => repo.FindAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Employee, bool>>>(), cancellationToken), Times.Once);
		}


		[Fact]
		public async Task Handle_Should_Return_TeamLeaders_Employees()
		{
			// Arrange
			var employees = new List<Employee>
				{
					new Employee { Id = Guid.NewGuid(), Name = "TeamLeader1", Role = EmployeeRole.TeamLeader },
					new Employee { Id = Guid.NewGuid(), Name = "TeamLeader2", Role = EmployeeRole.TeamLeader }
				};

			_mock
				.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<Employee, bool>> predicate, CancellationToken _) =>
					employees.AsQueryable().Where(predicate.Compile()).ToList());

			var query = new GetEmployeeTeamLeaderQuery();
			var cancellationToken = CancellationToken.None;

			// Act
			var result = await _getEmployeeTeamLeaderHandler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(employees.Count, result.Count());

			foreach (var employee in employees)
			{
				Assert.Contains(result, r => r.Id == employee.Id && r.Name == employee.Name);
			}

			// Verify that FindAsync was called once with the correct filter
			_mock.Verify(repo => repo.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>(), cancellationToken), Times.Once);
		}
	}
}
