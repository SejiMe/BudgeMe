using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class PromptTemplate
{
    public Guid Id { get; set; }
    public string TriggerCode { get; set; } = string.Empty;
    public PromptType PromptType { get; set; }
    public string MessageTemplate { get; set; } = string.Empty;
    public InsightSeverity Severity { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<UserPrompt> UserPrompts { get; set; } = new List<UserPrompt>();
}
