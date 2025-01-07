using DailyPulse.Application.CQRS.Commands.Locations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace DailyPulse.IntegrationTests.Location
{
	public class CreateLocationIntegrationTest  : BaseIntegrationTest
	{
		public CreateLocationIntegrationTest(DailyPulseWebApplicationFactory factory) 
			:base(factory)
		{
		}

		[Fact]
		public async Task CreateNewLocation_ShouldSucceed()
		{
			// Arrange: Define request payload

			var regionId = await applicationDbContext.Regions
				.Where(x => x.Name == "EGYPT")
				.Select(x => x.Id)
				.FirstOrDefaultAsync();

			var createLocationRequest = new CreateLocationCommand
			{
				Name = "Test Location",
				RegionId = regionId
			};

			var jsonContent = new StringContent(
				JsonSerializer.Serialize(createLocationRequest),
				Encoding.UTF8,
				"application/json"
			);

			// Act: Send the command via MediatR
			await sender.Send(createLocationRequest);

			// Assert: Validate the database changes or any expected side effect
			var locationExists = await applicationDbContext.Locations
				.AnyAsync(x => x.Name == "Test Location" && x.RegionId == regionId);
			locationExists.Should().BeTrue();
		}

		[Fact]
		public async Task CreateNewLocation_ShouldReturnCreated()
		{
			// Arrange: Prepare the request payload
			var regionId = await applicationDbContext.Regions
				.Where(x => x.Name == "EGYPT")
				.Select(x => x.Id)
				.FirstOrDefaultAsync();

			var createLocationRequest = new
			{
				Name = "Test Location",
				RegionId = regionId
			};

			var jsonContent = new StringContent(
				JsonSerializer.Serialize(createLocationRequest),
				Encoding.UTF8,
				"application/json"
			);

			// Act: Send POST request to the endpoint
			var response = await _httpClient.PostAsync("/api/Locations/CreateNewLocation", jsonContent);

			// Assert: Validate the response
			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}
	}
}
