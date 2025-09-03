using DailyPulse.Application.Common;
using DailyPulse.Domain.Entities;
using DailyPulse.Infrastructure.Persistence;
using DailyPulse.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DailyPulse.IntegrationTests
{
	public class PaginationTests
	{
		[Fact]
		public async System.Threading.Tasks.Task FindWithIncludePaginated_CapsPageSizeToMax()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;

			using var context = new ApplicationDbContext(options);

			// Seed 100 items
			if (!context.ScopeOfWorks.Any())
			{
				for (int i = 1; i <= 100; i++)
				{
					context.ScopeOfWorks.Add(new ScopeOfWork { Id = Guid.NewGuid() , Name = $"Test {i}"});
				}
				await context.SaveChangesAsync();
			}

			var repository = new GenericRepository<ScopeOfWork>(context);

			var requestParameters = new RequestParameters
			{
				PageNumber = 1,
				PageSize = 20
			};

			// Act
			var result = await repository.FindWithIncludePaginated(
				predicate: null,
				includes: null,
				requestParameters: requestParameters
			);

			// Assert
			Assert.Equal(20, result.Count); // Should cap at 20
			Assert.Equal(100, result.MetaData.TotalCount); // Total is still 100
		}

	}
}
