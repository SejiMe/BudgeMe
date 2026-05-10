using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class PromptTemplateConfiguration : IEntityTypeConfiguration<PromptTemplate>
{
    public void Configure(EntityTypeBuilder<PromptTemplate> builder)
    {
        builder.ToTable("prompt_templates");

        builder.HasKey(template => template.Id);

        builder.Property(template => template.TriggerCode).HasMaxLength(100);
        builder.Property(template => template.PromptType).HasConversion<string>().HasMaxLength(50);
        builder.Property(template => template.MessageTemplate).HasMaxLength(1_000);
        builder.Property(template => template.Severity).HasConversion<string>().HasMaxLength(50);

        builder.HasIndex(template => template.TriggerCode).IsUnique();
    }
}
