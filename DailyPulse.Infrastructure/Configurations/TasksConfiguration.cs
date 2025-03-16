using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class TasksConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
	{
		public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
				.HasDefaultValueSql("current_timestamp()");

			builder.Property(p => p.Name)
					   .IsRequired()
					   .HasMaxLength(500);

			builder.Property(p => p.EstimatedWorkingHours)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.CreatedByMachine)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.IsRejectedByAdmin)
			.HasDefaultValue(false);

			builder.Property(p => p.IsRejectedByEmployee)
			.HasDefaultValue(false);

			builder.Property(p => p.DrawingId)
			   .IsRequired()
			.HasMaxLength(500);

			builder.Property(p => p.DrawingTitle)
			 .IsRequired()
			.HasMaxLength(500);

			builder.Property(p => p.FilePath)
			   .IsRequired()
			.HasMaxLength(500);

			builder.Property(p => p.DateFrom)
			.IsRequired();

			builder.Property(p => p.DateTo)
				 .IsRequired();

			builder.Property(t => t.TaskTypeDetailsId)
				.IsRequired(false);

			builder.HasOne(p => p.Project)
				.WithMany(s => s.Tasks)
				.HasForeignKey(p => p.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);

			// Foreign Key for Employee
			builder.HasOne(p => p.Employee)
				.WithMany(e => e.Tasks)
				.HasForeignKey(p => p.EmpId)
				.OnDelete(DeleteBehavior.Restrict);

			// Define one-to-many relationship with TaskDetail
			builder.HasMany(t => t.TaskDetails)
				  .WithOne(td => td.Task)
				  .HasForeignKey(td => td.TaskId)
				  .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(t => t.TaskTypeDetails)
				  .WithMany(td => td.Tasks)
				  .HasForeignKey(td => td.TaskTypeDetailsId)
				  .OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(p => p.Name)
					  .IsUnique();
		}
	}
}
