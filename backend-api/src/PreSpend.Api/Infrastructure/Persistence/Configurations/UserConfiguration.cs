using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.FullName).HasMaxLength(200);
        builder.Property(user => user.Email).HasMaxLength(320);
        builder.Property(user => user.AvatarUrl).HasMaxLength(1_000);
        builder.Property(user => user.CurrencyCode).HasMaxLength(3).IsFixedLength();
        builder.Property(user => user.IncomeLevel).HasConversion<string>().HasMaxLength(50);

        builder.HasIndex(user => user.Email);

        builder.HasMany(user => user.AuthIdentities)
            .WithOne(identity => identity.User)
            .HasForeignKey(identity => identity.UserId);
    }
}
