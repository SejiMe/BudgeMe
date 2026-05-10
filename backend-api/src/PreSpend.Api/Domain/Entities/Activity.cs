using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class Activity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public ActivityType ActivityType { get; set; }
    public TriggerType TriggerType { get; set; }
    public DateOnly? PlannedDate { get; set; }
    public DateOnly? ActualDate { get; set; }
    public ActivityStatus Status { get; set; }
    public decimal? PlannedBudget { get; set; }
    public string? EmotionalContext { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public SpendingPlan? SpendingPlan { get; set; }
    public ICollection<CashflowEntry> CashflowEntries { get; set; } = new List<CashflowEntry>();
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    public ICollection<UserInsight> Insights { get; set; } = new List<UserInsight>();
    public ICollection<UserPrompt> Prompts { get; set; } = new List<UserPrompt>();
}
