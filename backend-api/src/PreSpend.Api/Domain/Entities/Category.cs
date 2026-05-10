using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class Category
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryType CategoryType { get; set; }
    public bool IsSystemDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<CashflowEntry> CashflowEntries { get; set; } = new List<CashflowEntry>();
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    public ICollection<SpendingPlanItem> SpendingPlanItems { get; set; } = new List<SpendingPlanItem>();
    public ICollection<SpendingPlanTemplateItem> SpendingPlanTemplateItems { get; set; } = new List<SpendingPlanTemplateItem>();
}
