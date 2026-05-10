using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class InsightRuleConfiguration : IEntityTypeConfiguration<InsightRule>
{
    public void Configure(EntityTypeBuilder<InsightRule> builder)
    {
        builder.ToTable("insight_rules");

        builder.HasKey(rule => rule.Id);

        builder.Property(rule => rule.RuleCode).HasMaxLength(100);
        builder.Property(rule => rule.RuleName).HasMaxLength(200);
        builder.Property(rule => rule.Description).HasMaxLength(1_000);
        builder.Property(rule => rule.Severity).HasConversion<string>().HasMaxLength(50);

        builder.HasIndex(rule => rule.RuleCode).IsUnique();
    }
}
