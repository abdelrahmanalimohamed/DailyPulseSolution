using DailyPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPulse.Infrastructure.Configurations;
internal sealed class ProfitCenterConfiguration : IEntityTypeConfiguration<ProfitCenter>
{
	public void Configure(EntityTypeBuilder<ProfitCenter> builder)
	{
		builder.HasKey(k => k.Id);

		builder.Property(x => x.CreatedDate)
			   .HasDefaultValueSql("current_timestamp()");

		builder.Property(p => p.ProfitCenterDescription)
				  .IsRequired()
				  .HasMaxLength(100);

		builder.HasIndex(p => p.ProfitCenterNumber)
				  .IsUnique();
	}
}