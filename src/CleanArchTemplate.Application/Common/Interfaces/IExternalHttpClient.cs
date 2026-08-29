namespace CleanArchTemplate.Application.Common.Interfaces;

/// <summary>
/// Porta para requisicoes HTTP externas. A implementacao base fica na Infrastructure.
/// Crie interfaces especificas por integracao quando houver regras proprias de contrato.
/// </summary>
public interface IExternalHttpClient
{
    Task<TResponse?> GetAsync<TResponse>(string relativeUrl, CancellationToken cancellationToken = default);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest body, CancellationToken cancellationToken = default);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string relativeUrl, TRequest body, CancellationToken cancellationToken = default);
    Task DeleteAsync(string relativeUrl, CancellationToken cancellationToken = default);
}
