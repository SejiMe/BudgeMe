using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.ToTable("line_items");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.LineType).HasConversion<string>().HasMaxLength(50);
        builder.Property(item => item.ItemName).HasMaxLength(200);
        builder.Property(item => item.Amount).HasPrecision(18, 2);
        builder.Property(item => item.Quantity).HasPrecision(18, 3);
        builder.Property(item => item.Classification).HasConversion<string>().HasMaxLength(50);
        builder.Property(item => item.Note).HasMaxLength(1_000);

        builder.HasIndex(item => new { item.UserId, item.OccurredAt });

        builder.HasOne(item => item.User)
            .WithMany(user => user.LineItems)
            .HasForeignKey(item => item.UserId);

        builder.HasOne(item => item.Activity)
            .WithMany(activity => activity.LineItems)
            .HasForeignKey(item => item.ActivityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(item => item.CashflowEntry)
            .WithMany(entry => entry.LineItems)
            .HasForeignKey(item => item.CashflowEntryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(item => item.SpendingPlanItem)
            .WithMany(planItem => planItem.LineItems)
            .HasForeignKey(item => item.SpendingPlanItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(item => item.Category)
            .WithMany(category => category.LineItems)
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
