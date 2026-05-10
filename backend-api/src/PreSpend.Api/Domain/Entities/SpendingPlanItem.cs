using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class SpendingPlanItem
{
    public Guid Id { get; set; }
    public Guid SpendingPlanId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public Classification Classification { get; set; }
    public int PriorityLevel { get; set; }
    public decimal PlannedAmount { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public SpendingPlan SpendingPlan { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}
