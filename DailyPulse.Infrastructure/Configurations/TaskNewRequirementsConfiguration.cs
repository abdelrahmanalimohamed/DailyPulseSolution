﻿using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class TaskNewRequirementsConfiguration : IEntityTypeConfiguration<TaskNewRequirements>
	{
		public void Configure(EntityTypeBuilder<TaskNewRequirements> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.HasOne(r => r.Task)
				.WithMany(t => t.TaskNewRequirements)
				.HasForeignKey(r => r.TaskId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(r => r.Employee)
				.WithMany(t => t.TaskNewRequirements)
				.HasForeignKey(r => r.CreatedBy)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(r => r.TaskId);
		}
	}
}