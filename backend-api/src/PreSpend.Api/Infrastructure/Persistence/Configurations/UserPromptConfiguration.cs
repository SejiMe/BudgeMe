using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserPromptConfiguration : IEntityTypeConfiguration<UserPrompt>
{
    public void Configure(EntityTypeBuilder<UserPrompt> builder)
    {
        builder.ToTable("user_prompts");

        builder.HasKey(prompt => prompt.Id);

        builder.Property(prompt => prompt.PromptType).HasConversion<string>().HasMaxLength(50);
        builder.Property(prompt => prompt.Message).HasMaxLength(1_000);
        builder.Property(prompt => prompt.Status).HasConversion<string>().HasMaxLength(50);
        builder.Property(prompt => prompt.ActionTaken).HasMaxLength(200);
        builder.Property(prompt => prompt.Metadata).HasColumnType("jsonb");

        builder.HasIndex(prompt => new { prompt.UserId, prompt.Status });

        builder.HasOne(prompt => prompt.User)
            .WithMany(user => user.Prompts)
            .HasForeignKey(prompt => prompt.UserId);

        builder.HasOne(prompt => prompt.PromptTemplate)
            .WithMany(template => template.UserPrompts)
            .HasForeignKey(prompt => prompt.PromptTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(prompt => prompt.RelatedActivity)
            .WithMany(activity => activity.Prompts)
            .HasForeignKey(prompt => prompt.RelatedActivityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
