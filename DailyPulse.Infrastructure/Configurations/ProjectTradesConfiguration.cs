using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class ProjectTradesConfiguration : IEntityTypeConfiguration<ProjectTrades>
	{
		public void Configure(EntityTypeBuilder<ProjectTrades> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
				   .HasDefaultValueSql("current_timestamp()");

			builder.HasOne(p => p.Project)
					.WithMany(e => e.ProjectTrades)
					.HasForeignKey(p => p.ProjectId)
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}