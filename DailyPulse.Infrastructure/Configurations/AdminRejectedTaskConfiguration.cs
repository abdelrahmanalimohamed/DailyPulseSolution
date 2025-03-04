using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class AdminRejectedTaskConfiguration : IEntityTypeConfiguration<AdminRejectedTask>
	{
		public void Configure(EntityTypeBuilder<AdminRejectedTask> builder)
		{
			builder.HasKey(k => k.Id);
			builder.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

			builder.HasOne(r => r.Task)
				.WithMany(t => t.TaskLogs)
				.HasForeignKey(r => r.TaskId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(r => r.TaskId);
		}
	}
}