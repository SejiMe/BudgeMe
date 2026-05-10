using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class InsightRule
{
    public Guid Id { get; set; }
    public string RuleCode { get; set; } = string.Empty;
    public string RuleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public InsightSeverity Severity { get; set; }
    public bool IsActive { get; set; }

    public ICollection<UserInsight> UserInsights { get; set; } = new List<UserInsight>();
}
