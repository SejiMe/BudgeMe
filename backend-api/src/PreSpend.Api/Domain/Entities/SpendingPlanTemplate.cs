using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class SpendingPlanTemplate
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public ActivityType ActivityType { get; set; }
    public bool IsSystemDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<SpendingPlanTemplateItem> Items { get; set; } = new List<SpendingPlanTemplateItem>();
    public ICollection<SpendingPlan> CreatedSpendingPlans { get; set; } = new List<SpendingPlan>();
}
