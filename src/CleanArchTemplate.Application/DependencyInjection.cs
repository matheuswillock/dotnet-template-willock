using CleanArchTemplate.Application.UseCases.Samples.CreateSample;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchTemplate.Application;

/// <summary>
/// Registre aqui use cases, validators e servicos de aplicacao.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateSampleUseCase, CreateSampleUseCase>();
        return services;
    }
}
