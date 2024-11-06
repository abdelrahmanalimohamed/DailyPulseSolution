using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.CommandHandler.LocationsHandlers;
using DailyPulse.Application.CQRS.Commands.Locations;
using DailyPulse.Domain.Entities;
using MediatR;
using Moq;
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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result); 

            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Location>(
                loc => loc.Name == command.Name && loc.RegionId == command.RegionId
            ), It.IsAny<CancellationToken>()), Times.Once); 
        }

    }
}
