using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class LineItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? ActivityId { get; set; }
    public Guid? CashflowEntryId { get; set; }
    public Guid? SpendingPlanItemId { get; set; }
    public LineItemType LineType { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Quantity { get; set; }
    public Guid? CategoryId { get; set; }
    public bool WasPlanned { get; set; }
    public Classification Classification { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset OccurredAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Activity? Activity { get; set; }
    public CashflowEntry? CashflowEntry { get; set; }
    public SpendingPlanItem? SpendingPlanItem { get; set; }
    public Category? Category { get; set; }
}
