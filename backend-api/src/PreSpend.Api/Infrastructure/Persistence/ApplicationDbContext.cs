using Microsoft.EntityFrameworkCore;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Infrastructure.Persistence.Seeders;

namespace PreSpend.Api.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserFinancialProfile> UserFinancialProfiles => Set<UserFinancialProfile>();
    public DbSet<UserAuthIdentity> UserAuthIdentities => Set<UserAuthIdentity>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<CashflowEntry> CashflowEntries => Set<CashflowEntry>();
    public DbSet<LineItem> LineItems => Set<LineItem>();
    public DbSet<SpendingPlan> SpendingPlans => Set<SpendingPlan>();
    public DbSet<SpendingPlanItem> SpendingPlanItems => Set<SpendingPlanItem>();
    public DbSet<SpendingPlanTemplate> SpendingPlanTemplates => Set<SpendingPlanTemplate>();
    public DbSet<SpendingPlanTemplateItem> SpendingPlanTemplateItems => Set<SpendingPlanTemplateItem>();
    public DbSet<InsightRule> InsightRules => Set<InsightRule>();
    public DbSet<UserInsight> UserInsights => Set<UserInsight>();
    public DbSet<PromptTemplate> PromptTemplates => Set<PromptTemplate>();
    public DbSet<UserPrompt> UserPrompts => Set<UserPrompt>();
    public DbSet<UserBehaviorMetric> UserBehaviorMetrics => Set<UserBehaviorMetric>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.SeedStableData();
    }
}
