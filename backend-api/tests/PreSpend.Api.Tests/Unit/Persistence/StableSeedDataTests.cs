using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Infrastructure.Persistence;
using PreSpend.Api.Infrastructure.Persistence.Seeders;

namespace PreSpend.Api.Tests.Unit.Persistence;

public sealed class StableSeedDataTests
{
    [Fact]
    public void Model_SeedsSystemCategories()
    {
        using var db = CreateDbContext();

        var seedData = GetSeedData<Category>(db);
        var names = seedData.Select(seed => seed["Name"]).ToArray();

        seedData.Should().HaveCount(SystemCategorySeeder.ExpectedCount);
        names.Should().BeEquivalentTo(
        [
            "groceries",
            "bills",
            "transport",
            "food-drink",
            "household",
            "health",
            "personal-care",
            "shopping",
            "social",
            "income",
            "other"
        ]);
        seedData.All(seed => seed["UserId"] is null).Should().BeTrue();
        seedData.All(seed => Equals(seed["IsSystemDefault"], true)).Should().BeTrue();
    }

    [Fact]
    public void Model_SeedsInsightRules()
    {
        using var db = CreateDbContext();

        var seedData = GetSeedData<InsightRule>(db);
        var ruleCodes = seedData.Select(seed => seed["RuleCode"]).ToArray();

        seedData.Should().HaveCount(InsightRuleSeeder.ExpectedCount);
        ruleCodes.Should().BeEquivalentTo(
        [
            "planned_over_actual",
            "actual_over_planned",
            "unplanned_purchase_detected",
            "want_heavy_activity",
            "neglected_essential"
        ]);
        seedData.All(seed => Equals(seed["IsActive"], true)).Should().BeTrue();
    }

    [Fact]
    public void Model_SeedsPromptTemplates()
    {
        using var db = CreateDbContext();

        var seedData = GetSeedData<PromptTemplate>(db);
        var triggerCodes = seedData.Select(seed => seed["TriggerCode"]).ToArray();

        seedData.Should().HaveCount(PromptTemplateSeeder.ExpectedCount);
        triggerCodes.Should().BeEquivalentTo(
        [
            "plan_activity",
            "reuse_template",
            "reflect_after_activity",
            "plan_after_overspend"
        ]);
        seedData.All(seed => Equals(seed["IsActive"], true)).Should().BeTrue();
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Host=localhost;Database=prespend_model_test;Username=postgres;Password=postgres")
            .UseSnakeCaseNamingConvention()
            .Options;

        return new ApplicationDbContext(options);
    }

    private static IReadOnlyList<IDictionary<string, object?>> GetSeedData<TEntity>(ApplicationDbContext db)
    {
        var designTimeModel = db.GetService<IDesignTimeModel>().Model;
        var entityType = designTimeModel.FindEntityType(typeof(TEntity));

        entityType.Should().NotBeNull();

        return entityType!.GetSeedData().ToArray();
    }
}
