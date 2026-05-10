using Microsoft.EntityFrameworkCore;

namespace PreSpend.Api.Infrastructure.Persistence.Seeders;

public static class StableSeedDataExtensions
{
    internal static readonly DateTimeOffset SeededAt = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public static void SeedStableData(this ModelBuilder modelBuilder)
    {
        SystemCategorySeeder.Seed(modelBuilder);
        InsightRuleSeeder.Seed(modelBuilder);
        PromptTemplateSeeder.Seed(modelBuilder);
    }
}
