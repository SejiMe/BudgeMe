using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class SpendingPlanItemConfiguration : IEntityTypeConfiguration<SpendingPlanItem>
{
    public void Configure(EntityTypeBuilder<SpendingPlanItem> builder)
    {
        builder.ToTable("spending_plan_items");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.ItemName).HasMaxLength(200);
        builder.Property(item => item.Classification).HasConversion<string>().HasMaxLength(50);
        builder.Property(item => item.PlannedAmount).HasPrecision(18, 2);
        builder.Property(item => item.Note).HasMaxLength(1_000);

        builder.HasOne(item => item.SpendingPlan)
            .WithMany(list => list.Items)
            .HasForeignKey(item => item.SpendingPlanId);

        builder.HasOne(item => item.Category)
            .WithMany(category => category.SpendingPlanItems)
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
