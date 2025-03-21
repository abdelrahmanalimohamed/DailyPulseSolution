using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
	{
		public void Configure(EntityTypeBuilder<Location> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.Property(l => l.Name)
				.IsRequired()
				.HasMaxLength(100);

			// Location belongs to a Region
			builder.HasOne(p => p.Region)
				.WithMany(r => r.Locations)
				.HasForeignKey(x => x.RegionId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(p => p.Name)
					  .IsUnique();
		}
	}
}