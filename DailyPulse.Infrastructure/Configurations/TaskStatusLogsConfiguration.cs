using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class TaskStatusLogsConfiguration : IEntityTypeConfiguration<TaskStatusLogs>
	{
		public void Configure(EntityTypeBuilder<TaskStatusLogs> builder)
		{

			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.Property(x => x.MachineName)
				   .HasMaxLength(100);

			builder.HasOne(r => r.Task)
				.WithMany(t => t.TaskStatusLogs)
				.HasForeignKey(r => r.TaskId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(r => r.TaskId);
		}
	}
}