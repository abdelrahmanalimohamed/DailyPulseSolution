using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
	{
		public void Configure(EntityTypeBuilder<Region> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.Property(r => r.Name)
					.IsRequired()
					.HasMaxLength(100);

			// 1:* Relationship with Locations
			builder.HasMany(r => r.Locations)
				.WithOne(x => x.Region)
				.HasForeignKey(u => u.RegionId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}