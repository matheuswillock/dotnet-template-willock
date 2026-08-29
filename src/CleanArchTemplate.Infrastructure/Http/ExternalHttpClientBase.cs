using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CleanArchTemplate.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CleanArchTemplate.Infrastructure.Http;

/// <summary>
/// Base para clientes HTTP externos. Herde esta classe para cada integracao e passe o nome da secao no construtor.
/// Trata respostas 200, 201, 400, 404 e 500+ com mensagens claras e configuracao via appsettings.
/// </summary>
public abstract class ExternalHttpClientBase : IExternalHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    protected ExternalHttpClientBase(HttpClient httpClient, IConfiguration configuration, string serviceName)
    {
        _httpClient = httpClient;
        var options = configuration.GetSection($"ExternalServices:{serviceName}").Get<ExternalHttpClientOptions>()
            ?? throw new InvalidOperationException($"External service configuration not found: {serviceName}.");

        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new InvalidOperationException($"External service BaseUrl is required: {serviceName}.");

        _httpClient.BaseAddress = new Uri(options.BaseUrl);

        if (!string.IsNullOrWhiteSpace(options.BearerToken))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.BearerToken);

        foreach (var header in options.Headers)
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
    }

    public async Task<TResponse?> GetAsync<TResponse>(string relativeUrl, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(relativeUrl, cancellationToken);
        return await HandleResponseAsync<TResponse>(response, cancellationToken);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string relativeUrl, TRequest body, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(relativeUrl, body, _jsonOptions, cancellationToken);
        return await HandleResponseAsync<TResponse>(response, cancellationToken);
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string relativeUrl, TRequest body, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(relativeUrl, body, _jsonOptions, cancellationToken);
        return await HandleResponseAsync<TResponse>(response, cancellationToken);
    }

    public async Task DeleteAsync(string relativeUrl, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(relativeUrl, cancellationToken);
        await HandleResponseAsync<object>(response, cancellationToken);
    }

    private async Task<TResponse?> HandleResponseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new ExternalHttpRequestException(response.StatusCode, "External service returned 400 Bad Request.", responseBody);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new ExternalHttpRequestException(response.StatusCode, "External service returned 404 Not Found.", responseBody);

        if ((int)response.StatusCode >= 500)
            throw new ExternalHttpRequestException(response.StatusCode, "External service returned 500+ Server Error.", responseBody);

        throw new ExternalHttpRequestException(response.StatusCode, $"External service returned {(int)response.StatusCode}.", responseBody);
    }
}
