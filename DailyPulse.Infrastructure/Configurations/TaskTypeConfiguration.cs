using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
	{
		public void Configure(EntityTypeBuilder<TaskType> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
				   .HasDefaultValueSql("current_timestamp()");

			builder.Property(e => e.Name)
				.IsRequired()
			    .HasMaxLength(100);

			builder.HasMany(t => t.TaskTypeDetails)
			  .WithOne(td => td.TaskType)
			  .HasForeignKey(td => td.TaskTypeId)
			  .OnDelete(DeleteBehavior.Cascade);
		}
	}
}