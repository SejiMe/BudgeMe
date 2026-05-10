using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSpend.Api.Domain.Entities;

namespace PreSpend.Api.Infrastructure.Persistence.Configurations;

public sealed class UserAuthIdentityConfiguration : IEntityTypeConfiguration<UserAuthIdentity>
{
    public void Configure(EntityTypeBuilder<UserAuthIdentity> builder)
    {
        builder.ToTable("user_auth_identities");

        builder.HasKey(identity => identity.Id);

        builder.Property(identity => identity.Provider).HasMaxLength(100);
        builder.Property(identity => identity.ProviderUserId).HasMaxLength(200);
        builder.Property(identity => identity.ProviderEmail).HasMaxLength(320);

        builder.HasIndex(identity => new { identity.Provider, identity.ProviderUserId }).IsUnique();
    }
}
