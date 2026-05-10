using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserInsightConfiguration : IEntityTypeConfiguration<UserInsight>
{
    public void Configure(EntityTypeBuilder<UserInsight> builder)
    {
        builder.ToTable("user_insights");

        builder.HasKey(insight => insight.Id);

        builder.Property(insight => insight.InsightTitle).HasMaxLength(200);
        builder.Property(insight => insight.InsightMessage).HasMaxLength(1_000);
        builder.Property(insight => insight.Severity).HasConversion<string>().HasMaxLength(50);
        builder.Property(insight => insight.Metadata).HasColumnType("jsonb");

        builder.HasOne(insight => insight.User)
            .WithMany(user => user.Insights)
            .HasForeignKey(insight => insight.UserId);

        builder.HasOne(insight => insight.Activity)
            .WithMany(activity => activity.Insights)
            .HasForeignKey(insight => insight.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(insight => insight.Rule)
            .WithMany(rule => rule.UserInsights)
            .HasForeignKey(insight => insight.RuleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
