using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PreSpend.Api.Common.Configuration;

namespace PreSpend.Api.Infrastructure.Persistence;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var projectRoot = Directory.GetCurrentDirectory();
        var envPath = Path.GetFullPath(Path.Combine(projectRoot, ".env"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectRoot)
            .AddJsonFile("src/PreSpend.Api/appsettings.json", optional: true)
            .AddJsonFile("src/PreSpend.Api/appsettings.Development.json", optional: true)
            .AddEnvFile(envPath)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .Options;

        return new ApplicationDbContext(options);
    }
}
