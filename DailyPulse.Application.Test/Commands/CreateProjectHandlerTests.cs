﻿using DailyPulse.Application.Abstraction;
using DailyPulse.Application.CQRS.CommandHandler.ProjectsHandlers;
using DailyPulse.Application.CQRS.Commands.Projects;
using DailyPulse.Domain.Entities;
using MediatR;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace DailyPulse.Application.Test.Commands
{
    public class CreateProjectHandlerTests
    {
        private readonly Mock<IGenericRepository<Project>> _mockRepository;
        private readonly CreateProjectHandler _handler;
        public CreateProjectHandlerTests()
        {
            _mockRepository = new Mock<IGenericRepository<Project>>();
            _handler = new CreateProjectHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddLocationAndReturnUnitValue_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateProjectCommand {
                Name = "New Project", 
                RegionId = Guid.NewGuid() , 
                Drawings = "Drawing Test " ,
                LocationId = Guid.NewGuid()  ,
                Description = "Description Test",
                ScopeOfWorkId = Guid.NewGuid() , 
                TeamLeadId = Guid.NewGuid() 
            };

            _mockRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Project>(
                proj => proj.Name == command.Name && 
                proj.RegionId == command.RegionId &&
                proj.Drawings == command.Drawings &&
                proj.LocationId == command.LocationId && 
                proj.Description == command.Description
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}