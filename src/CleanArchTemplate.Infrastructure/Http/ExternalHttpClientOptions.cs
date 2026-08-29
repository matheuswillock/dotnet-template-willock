namespace CleanArchTemplate.Infrastructure.Http;

/// <summary>
/// Configuracao lida de appsettings: ExternalServices:{ServiceName}.
/// Inclua BaseUrl, BearerToken e headers padrao da integracao externa.
/// </summary>
public sealed class ExternalHttpClientOptions
{
    public string BaseUrl { get; init; } = string.Empty;
    public string? BearerToken { get; init; }
    public Dictionary<string, string> Headers { get; init; } = [];
}
