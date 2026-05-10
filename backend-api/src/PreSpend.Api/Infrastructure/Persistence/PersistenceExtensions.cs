using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PreSpend.Api.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services
            .AddOptions<PostgresOptions>()
            .Bind(configuration.GetSection(PostgresOptions.SectionName))
            .Validate(
                options => environment.IsDevelopment() || !string.IsNullOrWhiteSpace(options.DefaultConnection),
                "ConnectionStrings:DefaultConnection is required outside Development.")
            .ValidateOnStart();

        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            var postgresOptions = provider.GetRequiredService<IOptions<PostgresOptions>>().Value;
            var connectionString = postgresOptions.DefaultConnection ?? string.Empty;

            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
