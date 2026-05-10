using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class SpendingPlanTemplateConfiguration : IEntityTypeConfiguration<SpendingPlanTemplate>
{
    public void Configure(EntityTypeBuilder<SpendingPlanTemplate> builder)
    {
        builder.ToTable("spending_plan_templates");

        builder.HasKey(template => template.Id);

        builder.Property(template => template.TemplateName).HasMaxLength(200);
        builder.Property(template => template.ActivityType).HasConversion<string>().HasMaxLength(50);

        builder.HasOne(template => template.User)
            .WithMany(user => user.SpendingPlanTemplates)
            .HasForeignKey(template => template.UserId);
    }
}
