using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class UserPrompt
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PromptTemplateId { get; set; }
    public Guid RelatedActivityId { get; set; }
    public PromptType PromptType { get; set; }
    public string Message { get; set; } = string.Empty;
    public PromptStatus Status { get; set; }
    public DateTimeOffset? ScheduledFor { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }
    public DateTimeOffset? SeenAt { get; set; }
    public DateTimeOffset? ActedAt { get; set; }
    public DateTimeOffset? DismissedAt { get; set; }
    public string? ActionTaken { get; set; }
    public string Metadata { get; set; } = "{}";
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public PromptTemplate PromptTemplate { get; set; } = null!;
    public Activity RelatedActivity { get; set; } = null!;
}
