using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.Queries.Locations;
using DailyPulse.Application.CQRS.QueriesHandler.LocationHandlers;
using DailyPulse.Domain.Entities;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.Test.Queries
{
    public class GetLocationByIdHandlerTests
    {
        private readonly Mock<IGenericRepository<Location>> _mockRepository;
        private readonly GetLocationByIdHandler _handler;

        public GetLocationByIdHandlerTests()
        {
            _mockRepository = new Mock<IGenericRepository<Location>>();
            _handler = new GetLocationByIdHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task HandleShouldReturnLocationWhenLocationExists()
        {
            // Arrange
            var query = new GetLocationByIdQuery { LocationId = Guid.NewGuid() };
            var location = new Location { Id = Guid.NewGuid(), Name = "Test Location", RegionId = Guid.NewGuid() };

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(query.LocationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(location);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(location.Id, result.Id); // Check that the returned location has the correct ID
            Assert.Equal(location.Name, result.Name); // Check that the name matches
            _mockRepository.Verify(repo => repo.GetByIdAsync(query.LocationId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenLocationDoesNotExist()
        {
            // Arrange
            var query = new GetLocationByIdQuery { LocationId = Guid.NewGuid() };

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(query.LocationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Location)null); 

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result); // Check that the result is null when no location is found
            _mockRepository.Verify(repo => repo.GetByIdAsync(query.LocationId, It.IsAny<CancellationToken>()), Times.Once); // Ensure GetByIdAsync was called once
        }
    }
}
