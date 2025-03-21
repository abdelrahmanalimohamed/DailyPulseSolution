using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using Moq;
using System.Data;
using System.Linq.Expressions;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.Test.Commands
{
	public class CreateLocationHandlerTests
    {
        private readonly Mock<IGenericRepository<Location>> _mockRepository;
        private readonly CreateLocationHandler _handler;

        public CreateLocationHandlerTests()
        {
            _mockRepository = new Mock<IGenericRepository<Location>>();
            _handler = new CreateLocationHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddLocationAndReturnUnitValue_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateLocationCommand { Name = "New Location", RegionId = Guid.NewGuid() };

            _mockRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Location>(
                loc => loc.Name == command.Name && loc.RegionId == command.RegionId
            ), It.IsAny<CancellationToken>()), Times.Once); 
        }

		[Fact]
		public async Task AddLocation_ShouldThrowException_WhenNameIsDuplicate()
		{
			// Arrange
			var command = new CreateLocationCommand { Name = "Duplicate Location", RegionId = Guid.NewGuid() };

			_mockRepository
				.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<Location, bool>>>(), CancellationToken.None))
				.ReturnsAsync(new Location { Name = "Duplicate Location" });  // Simulate duplicate location

			// Act & Assert
			var exception = await Assert.ThrowsAsync<DuplicateNameException>(() => _handler.Handle(command, CancellationToken.None));

			Assert.Equal("A location with the same name already exists.", exception.Message);
		}

	}
}
