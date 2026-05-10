using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class CashflowEntryConfiguration : IEntityTypeConfiguration<CashflowEntry>
{
    public void Configure(EntityTypeBuilder<CashflowEntry> builder)
    {
        builder.ToTable("cashflow_entries");

        builder.HasKey(entry => entry.Id);

        builder.Property(entry => entry.EntryType).HasConversion<string>().HasMaxLength(50);
        builder.Property(entry => entry.Amount).HasPrecision(18, 2);
        builder.Property(entry => entry.IncomeType).HasConversion<string>().HasMaxLength(50);
        builder.Property(entry => entry.ExpenseType).HasConversion<string>().HasMaxLength(50);
        builder.Property(entry => entry.SourceOrPayee).HasMaxLength(200);
        builder.Property(entry => entry.Note).HasMaxLength(1_000);

        builder.HasIndex(entry => new { entry.UserId, entry.EntryDate });

        builder.HasOne(entry => entry.User)
            .WithMany(user => user.CashflowEntries)
            .HasForeignKey(entry => entry.UserId);

        builder.HasOne(entry => entry.Activity)
            .WithMany(activity => activity.CashflowEntries)
            .HasForeignKey(entry => entry.ActivityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(entry => entry.Category)
            .WithMany(category => category.CashflowEntries)
            .HasForeignKey(entry => entry.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
