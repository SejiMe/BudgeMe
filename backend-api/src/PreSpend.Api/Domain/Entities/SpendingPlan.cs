using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class SpendingPlan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ActivityId { get; set; }
    public string Title { get; set; } = string.Empty;
    public SpendingPlanStatus Status { get; set; }
    public Guid? CreatedFromTemplateId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public SpendingPlanTemplate? CreatedFromTemplate { get; set; }
    public ICollection<SpendingPlanItem> Items { get; set; } = new List<SpendingPlanItem>();
}
