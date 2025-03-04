using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class ReAssignConfiguration : IEntityTypeConfiguration<ReAssign>
	{
		public void Configure(EntityTypeBuilder<ReAssign> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.HasOne(r => r.Employee)
				  .WithMany(e => e.ReAssigns)
				  .HasForeignKey(r => r.EmpId)
				  .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

			builder.HasOne(r => r.TeamLead)
				.WithMany()
				.HasForeignKey(r => r.TeamLeadId)
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

			builder.HasOne(r => r.Task)
				.WithMany(t => t.ReAssigns)
				.HasForeignKey(r => r.TaskId)
				.OnDelete(DeleteBehavior.Cascade); // Adjust as necessary

			builder.HasIndex(r => r.TaskId);
		}
	}
}