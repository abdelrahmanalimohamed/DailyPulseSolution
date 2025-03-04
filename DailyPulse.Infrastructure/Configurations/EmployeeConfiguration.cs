using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using static Dapper.SqlMapper;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate).HasDefaultValueSql("current_timestamp()");

			builder.Property(e => e.Title)
				.IsRequired()
			.HasMaxLength(100);

			builder.Property(e => e.Role)
			.IsRequired();

			builder.Property(e => e.IsAdmin)
				.HasDefaultValue(false);

			// Self-referencing relationship for reporting structure
			builder.HasOne(e => e.ReportTo)
				.WithMany(e => e.DirectReports)
				.HasForeignKey(e => e.ReportToId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(e => e.Tasks)
			   .WithOne(p => p.Employee)
			   .HasForeignKey(p => p.EmpId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(p => p.Name)
					  .IsUnique();
		}
	}
}
