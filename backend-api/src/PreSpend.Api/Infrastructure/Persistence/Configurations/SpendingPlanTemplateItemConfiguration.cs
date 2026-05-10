using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class SpendingPlanTemplateItemConfiguration : IEntityTypeConfiguration<SpendingPlanTemplateItem>
{
    public void Configure(EntityTypeBuilder<SpendingPlanTemplateItem> builder)
    {
        builder.ToTable("spending_plan_template_items");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.ItemName).HasMaxLength(200);
        builder.Property(item => item.Classification).HasConversion<string>().HasMaxLength(50);
        builder.Property(item => item.EstimatedAmount).HasPrecision(18, 2);

        builder.HasOne(item => item.Template)
            .WithMany(template => template.Items)
            .HasForeignKey(item => item.TemplateId);

        builder.HasOne(item => item.Category)
            .WithMany(category => category.SpendingPlanTemplateItems)
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
