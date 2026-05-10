using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class UserBehaviorMetric
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BehaviorMetricPeriodType PeriodType { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public int SpendingPlansCreatedCount { get; set; }
    public int ActivitiesCompletedCount { get; set; }
    public decimal PlannedSpendingTotal { get; set; }
    public decimal ActualSpendingTotal { get; set; }
    public int ImpulsePurchaseCount { get; set; }
    public decimal ImpulsePurchaseTotal { get; set; }
    public int NeglectedEssentialCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
