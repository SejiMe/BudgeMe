using Microsoft.EntityFrameworkCore;

namespace PreSpend.Api.Infrastructure.Persistence;

public static class NpgsqlOptionsExtensions
{
    private const string MigrationsHistoryTable = "ef_migrations_history";

    public static DbContextOptionsBuilder UsePreSpendNpgsql(
        this DbContextOptionsBuilder options,
        string connectionString)
    {
        return options
            .UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(MigrationsHistoryTable))
            .UseSnakeCaseNamingConvention();
    }

    public static DbContextOptionsBuilder<TContext> UsePreSpendNpgsql<TContext>(
        this DbContextOptionsBuilder<TContext> options,
        string connectionString)
        where TContext : DbContext
    {
        return options
            .UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(MigrationsHistoryTable))
            .UseSnakeCaseNamingConvention();
    }
}
