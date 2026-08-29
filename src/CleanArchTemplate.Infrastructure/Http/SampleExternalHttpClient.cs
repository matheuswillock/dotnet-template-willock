using Microsoft.Extensions.Configuration;

namespace CleanArchTemplate.Infrastructure.Http;

/// <summary>
/// Exemplo de cliente externo. Renomeie para a integracao real e implemente metodos de negocio aqui.
/// A configuracao fica em appsettings.json em ExternalServices:SampleService.
/// </summary>
public sealed class SampleExternalHttpClient(HttpClient httpClient, IConfiguration configuration)
    : ExternalHttpClientBase(httpClient, configuration, "SampleService");
