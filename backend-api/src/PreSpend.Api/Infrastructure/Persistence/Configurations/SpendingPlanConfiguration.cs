using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class SpendingPlanConfiguration : IEntityTypeConfiguration<SpendingPlan>
{
    public void Configure(EntityTypeBuilder<SpendingPlan> builder)
    {
        builder.ToTable("spending_plans");

        builder.HasKey(list => list.Id);

        builder.Property(list => list.Title).HasMaxLength(200);
        builder.Property(list => list.Status).HasConversion<string>().HasMaxLength(50);

        builder.HasIndex(list => list.ActivityId).IsUnique();

        builder.HasOne(list => list.User)
            .WithMany(user => user.SpendingPlans)
            .HasForeignKey(list => list.UserId);

        builder.HasOne(list => list.Activity)
            .WithOne(activity => activity.SpendingPlan)
            .HasForeignKey<SpendingPlan>(list => list.ActivityId);

        builder.HasOne(list => list.CreatedFromTemplate)
            .WithMany(template => template.CreatedSpendingPlans)
            .HasForeignKey(list => list.CreatedFromTemplateId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
