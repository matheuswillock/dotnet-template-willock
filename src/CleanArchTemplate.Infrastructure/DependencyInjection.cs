using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Infrastructure.Http;
using CleanArchTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchTemplate.Infrastructure;

/// <summary>
/// Registre aqui banco de dados, repositorios, clientes externos e adaptadores de infraestrutura.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            // Development usa InMemory para permitir rodar pelo Rider sem PostgreSQL local.
            // Docker/producao usam PostgreSQL por padrao. Altere Database:Provider em appsettings se precisar.
            var provider = configuration["Database:Provider"] ?? "PostgreSql";

            if (provider.Equals("InMemory", StringComparison.OrdinalIgnoreCase))
                options.UseInMemoryDatabase("CleanArchTemplateDb");
            else
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddHttpClient<SampleExternalHttpClient>();

        return services;
    }
}
