using Microsoft.EntityFrameworkCore;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Infrastructure.Persistence.Seeders;

public static class PromptTemplateSeeder
{
    public const int ExpectedCount = 4;

    public static readonly Guid PlanActivityId = Guid.Parse("33333333-3333-3333-3333-333333333001");
    public static readonly Guid ReuseTemplateId = Guid.Parse("33333333-3333-3333-3333-333333333002");
    public static readonly Guid ReflectAfterActivityId = Guid.Parse("33333333-3333-3333-3333-333333333003");
    public static readonly Guid PlanAfterOverspendId = Guid.Parse("33333333-3333-3333-3333-333333333004");

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromptTemplate>().HasData(
            Create(
                PlanActivityId,
                "plan_activity",
                PromptType.PlanActivity,
                "Planning {activityType} today?",
                InsightSeverity.Info),
            Create(
                ReuseTemplateId,
                "reuse_template",
                PromptType.ReuseTemplate,
                "Reuse your last {activityType} plan?",
                InsightSeverity.Info),
            Create(
                ReflectAfterActivityId,
                "reflect_after_activity",
                PromptType.ReflectAfterActivity,
                "How did this activity compare with your plan?",
                InsightSeverity.Info),
            Create(
                PlanAfterOverspendId,
                "plan_after_overspend",
                PromptType.PlanAfterOverspend,
                "You spent more than planned last time. Want to plan this one?",
                InsightSeverity.Warning));
    }

    private static PromptTemplate Create(
        Guid id,
        string triggerCode,
        PromptType promptType,
        string messageTemplate,
        InsightSeverity severity)
    {
        return new PromptTemplate
        {
            Id = id,
            TriggerCode = triggerCode,
            PromptType = promptType,
            MessageTemplate = messageTemplate,
            Severity = severity,
            IsActive = true,
            CreatedAt = StableSeedDataExtensions.SeededAt
        };
    }
}
