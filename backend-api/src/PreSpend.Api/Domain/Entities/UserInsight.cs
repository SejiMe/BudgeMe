using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class UserInsight
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ActivityId { get; set; }
    public Guid RuleId { get; set; }
    public string InsightTitle { get; set; } = string.Empty;
    public string InsightMessage { get; set; } = string.Empty;
    public InsightSeverity Severity { get; set; }
    public string Metadata { get; set; } = "{}";
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? SeenAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public InsightRule Rule { get; set; } = null!;
}
