using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserBehaviorMetricConfiguration : IEntityTypeConfiguration<UserBehaviorMetric>
{
    public void Configure(EntityTypeBuilder<UserBehaviorMetric> builder)
    {
        builder.ToTable("user_behavior_metrics");

        builder.HasKey(metric => metric.Id);

        builder.Property(metric => metric.PeriodType).HasConversion<string>().HasMaxLength(50);
        builder.Property(metric => metric.SpendingPlansCreatedCount).HasColumnName("audits_created_count");
        builder.Property(metric => metric.PlannedSpendingTotal).HasPrecision(18, 2);
        builder.Property(metric => metric.ActualSpendingTotal).HasPrecision(18, 2);
        builder.Property(metric => metric.ImpulsePurchaseTotal).HasPrecision(18, 2);

        builder.HasIndex(metric => new { metric.UserId, metric.PeriodType, metric.PeriodStart, metric.PeriodEnd })
            .IsUnique();

        builder.HasOne(metric => metric.User)
            .WithMany(user => user.BehaviorMetrics)
            .HasForeignKey(metric => metric.UserId);
    }
}
