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
            // Provider padrao: PostgreSQL. Ajuste a connection string em appsettings.json ou variaveis de ambiente.
            // Para outro banco, troque o provider aqui e o pacote NuGet no projeto Infrastructure.
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddHttpClient<SampleExternalHttpClient>();

        return services;
    }
}
