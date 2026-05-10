using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserFinancialProfileConfiguration : IEntityTypeConfiguration<UserFinancialProfile>
{
    public void Configure(EntityTypeBuilder<UserFinancialProfile> builder)
    {
        builder.ToTable("user_financial_profiles");

        builder.HasKey(profile => profile.Id);

        builder.Property(profile => profile.IncomeFrequency).HasConversion<string>().HasMaxLength(50);
        builder.Property(profile => profile.Timezone).HasMaxLength(100);

        builder.HasIndex(profile => profile.UserId).IsUnique();

        builder.HasOne(profile => profile.User)
            .WithOne(user => user.FinancialProfile)
            .HasForeignKey<UserFinancialProfile>(profile => profile.UserId);
    }
}
