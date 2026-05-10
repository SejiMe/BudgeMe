using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class CashflowEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? ActivityId { get; set; }
    public CashflowEntryType EntryType { get; set; }
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
    public IncomeType? IncomeType { get; set; }
    public ExpenseType? ExpenseType { get; set; }
    public string? SourceOrPayee { get; set; }
    public string? Note { get; set; }
    public DateOnly EntryDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Activity? Activity { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}
