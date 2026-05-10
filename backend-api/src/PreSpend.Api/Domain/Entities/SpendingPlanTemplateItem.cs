using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class SpendingPlanTemplateItem
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public Classification Classification { get; set; }
    public int PriorityLevel { get; set; }
    public decimal EstimatedAmount { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public SpendingPlanTemplate Template { get; set; } = null!;
    public Category? Category { get; set; }
}
