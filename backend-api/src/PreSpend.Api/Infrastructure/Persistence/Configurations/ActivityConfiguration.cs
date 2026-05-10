using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("activities");

        builder.HasKey(activity => activity.Id);

        builder.Property(activity => activity.Title).HasMaxLength(200);
        builder.Property(activity => activity.ActivityType).HasConversion<string>().HasMaxLength(50);
        builder.Property(activity => activity.TriggerType).HasConversion<string>().HasMaxLength(50);
        builder.Property(activity => activity.Status).HasConversion<string>().HasMaxLength(50);
        builder.Property(activity => activity.PlannedBudget).HasPrecision(18, 2);
        builder.Property(activity => activity.EmotionalContext).HasMaxLength(1_000);

        builder.HasIndex(activity => new { activity.UserId, activity.PlannedDate });

        builder.HasOne(activity => activity.User)
            .WithMany(user => user.Activities)
            .HasForeignKey(activity => activity.UserId);
    }
}
