namespace PreSpend.Api.Infrastructure.Persistence;

public sealed class PostgresOptions
{
    public const string SectionName = "ConnectionStrings";

    public string? DefaultConnection { get; init; }
    public string? MigrationConnection { get; init; }
}
