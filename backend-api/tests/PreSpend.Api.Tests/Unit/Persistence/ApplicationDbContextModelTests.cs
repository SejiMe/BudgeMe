using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Infrastructure.Persistence;

namespace PreSpend.Api.Tests.Unit.Persistence;

public sealed class ApplicationDbContextModelTests
{
    [Fact]
    public void Model_IncludesMvpEntities()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Host=localhost;Database=prespend_model_test;Username=postgres;Password=postgres")
            .UseSnakeCaseNamingConvention()
            .Options;

        using var db = new ApplicationDbContext(options);

        var entityTypes = db.Model.GetEntityTypes()
            .Select(entityType => entityType.ClrType)
            .ToArray();

        entityTypes.Should().Contain(
        [
            typeof(User),
            typeof(UserFinancialProfile),
            typeof(UserAuthIdentity),
            typeof(Category),
            typeof(Activity),
            typeof(CashflowEntry),
            typeof(LineItem),
            typeof(SpendingPlan),
            typeof(SpendingPlanItem),
            typeof(SpendingPlanTemplate),
            typeof(SpendingPlanTemplateItem),
            typeof(InsightRule),
            typeof(UserInsight),
            typeof(PromptTemplate),
            typeof(UserPrompt),
            typeof(UserBehaviorMetric)
        ]);
    }
}
