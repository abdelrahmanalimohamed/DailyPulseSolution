using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations
{
	internal sealed class ScopeOfWorkConfiguration : IEntityTypeConfiguration<ScopeOfWork>
	{
		public void Configure(EntityTypeBuilder<ScopeOfWork> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(x => x.CreatedDate)
					.HasDefaultValueSql("current_timestamp()");

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
