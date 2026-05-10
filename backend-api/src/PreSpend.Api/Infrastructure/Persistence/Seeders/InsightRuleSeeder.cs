using Microsoft.EntityFrameworkCore;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Infrastructure.Persistence.Seeders;

public static class InsightRuleSeeder
{
    public const int ExpectedCount = 5;

    public static readonly Guid PlannedOverActualId = Guid.Parse("22222222-2222-2222-2222-222222222001");
    public static readonly Guid ActualOverPlannedId = Guid.Parse("22222222-2222-2222-2222-222222222002");
    public static readonly Guid UnplannedPurchaseDetectedId = Guid.Parse("22222222-2222-2222-2222-222222222003");
    public static readonly Guid WantHeavyActivityId = Guid.Parse("22222222-2222-2222-2222-222222222004");
    public static readonly Guid NeglectedEssentialId = Guid.Parse("22222222-2222-2222-2222-222222222005");

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InsightRule>().HasData(
            Create(
                PlannedOverActualId,
                "planned_over_actual",
                "Planned under actual spend",
                "Highlights activities where actual spend stayed below the planned budget.",
                InsightSeverity.Positive),
            Create(
                ActualOverPlannedId,
                "actual_over_planned",
                "Actual over planned spend",
                "Highlights activities where actual spend exceeded the planned budget.",
                InsightSeverity.Warning),
            Create(
                UnplannedPurchaseDetectedId,
                "unplanned_purchase_detected",
                "Unplanned purchase detected",
                "Highlights actual line items that were not part of the spending plan.",
                InsightSeverity.Warning),
            Create(
                WantHeavyActivityId,
                "want_heavy_activity",
                "Want-heavy activity",
                "Highlights completed activities where want-classified items outweighed need-classified items.",
                InsightSeverity.Info),
            Create(
                NeglectedEssentialId,
                "neglected_essential",
                "Neglected essential",
                "Highlights need-classified planned items that were not purchased during the activity.",
                InsightSeverity.Info));
    }

    private static InsightRule Create(
        Guid id,
        string ruleCode,
        string ruleName,
        string description,
        InsightSeverity severity)
    {
        return new InsightRule
        {
            Id = id,
            RuleCode = ruleCode,
            RuleName = ruleName,
            Description = description,
            Severity = severity,
            IsActive = true
        };
    }
}
