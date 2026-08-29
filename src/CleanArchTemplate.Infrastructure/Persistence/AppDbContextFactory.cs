using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchTemplate.Infrastructure.Persistence;

/// <summary>
/// Factory usada pelo comando `dotnet ef` em tempo de design.
/// Ela sempre usa PostgreSQL para migrations, mesmo quando o ambiente Development usa InMemory para rodar rapido no Rider.
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var startupProjectPath = ResolveStartupProjectPath();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(startupProjectPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }

    private static string ResolveStartupProjectPath()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "src", "CleanArchTemplate.Api", "appsettings.json");

            if (File.Exists(candidate))
                return Path.GetDirectoryName(candidate)!;

            directory = directory.Parent;
        }

        return Directory.GetCurrentDirectory();
    }
}
