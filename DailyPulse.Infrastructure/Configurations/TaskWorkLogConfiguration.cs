using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class TaskWorkLogConfiguration : IEntityTypeConfiguration<TaskWorkLog>
	{
		public void Configure(EntityTypeBuilder<TaskWorkLog> builder)
		{
			builder.HasKey(k => k.Id);
			builder.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

			builder.Property(x => x.EndTime).HasDefaultValue(null);

			builder.HasOne(p => p.Task)
				.WithMany(r => r.TaskDetails)
				.HasForeignKey(x => x.TaskId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}