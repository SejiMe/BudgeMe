using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string CurrencyCode { get; set; } = "USD";
    public IncomeLevel? IncomeLevel { get; set; }
    public bool FamilySupportFlag { get; set; }
    public bool OnboardingCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public UserFinancialProfile? FinancialProfile { get; set; }
    public ICollection<UserAuthIdentity> AuthIdentities { get; set; } = new List<UserAuthIdentity>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public ICollection<CashflowEntry> CashflowEntries { get; set; } = new List<CashflowEntry>();
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    public ICollection<SpendingPlan> SpendingPlans { get; set; } = new List<SpendingPlan>();
    public ICollection<SpendingPlanTemplate> SpendingPlanTemplates { get; set; } = new List<SpendingPlanTemplate>();
    public ICollection<UserInsight> Insights { get; set; } = new List<UserInsight>();
    public ICollection<UserPrompt> Prompts { get; set; } = new List<UserPrompt>();
    public ICollection<UserBehaviorMetric> BehaviorMetrics { get; set; } = new List<UserBehaviorMetric>();
}
