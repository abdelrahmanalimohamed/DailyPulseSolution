using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class ProjectsConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
				   .HasDefaultValueSql("current_timestamp()");

			builder.Property(p => p.Name)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(e => e.Trade)
				   .IsRequired();

			builder.Property(p => p.Description)
				.IsRequired()
			.HasMaxLength(500);

			builder.HasOne(p => p.Region)
				.WithMany(s => s.Projects)
				.HasForeignKey(p => p.RegionId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(p => p.Location)
				.WithMany(s => s.Projects)
				.HasForeignKey(p => p.LocationId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(p => p.Employee)
					.WithMany(s => s.Projects)
					.HasForeignKey(p => p.EmployeeId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(p => p.Name)
					  .IsUnique();
		}
	}
}
