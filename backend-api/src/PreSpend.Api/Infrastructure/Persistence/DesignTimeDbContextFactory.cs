using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PreSpend.Api.Common.Configuration;

namespace PreSpend.Api.Infrastructure.Persistence;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var backendRoot = ResolveBackendRoot(Directory.GetCurrentDirectory());
        var envPath = Path.GetFullPath(Path.Combine(backendRoot, ".env"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(backendRoot)
            .AddJsonFile("src/PreSpend.Api/appsettings.json", optional: true)
            .AddJsonFile("src/PreSpend.Api/appsettings.Development.json", optional: true)
            .AddEnvFile(envPath)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("MigrationConnection")
            ?? configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"ConnectionStrings:MigrationConnection or ConnectionStrings:DefaultConnection is required for EF migrations. Checked .env path: {envPath}");
        }

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UsePreSpendNpgsql(connectionString)
            .Options;

        return new ApplicationDbContext(options);
    }

    private static string ResolveBackendRoot(string currentDirectory)
    {
        var directory = new DirectoryInfo(currentDirectory);

        while (directory is not null)
        {
            if (Directory.Exists(Path.Combine(directory.FullName, "src", "PreSpend.Api")))
            {
                return directory.FullName;
            }

            var backendApiPath = Path.Combine(directory.FullName, "backend-api");
            if (Directory.Exists(Path.Combine(backendApiPath, "src", "PreSpend.Api")))
            {
                return backendApiPath;
            }

            directory = directory.Parent;
        }

        return currentDirectory;
    }
}
