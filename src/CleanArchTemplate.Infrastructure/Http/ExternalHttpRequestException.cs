using System.Net;

namespace CleanArchTemplate.Infrastructure.Http;

/// <summary>
/// Excecao padrao para erros de integracoes HTTP externas.
/// Use StatusCode e ResponseBody para log e diagnostico, sem vazar dados sensiveis para a API.
/// </summary>
public sealed class ExternalHttpRequestException(HttpStatusCode statusCode, string message, string? responseBody = null)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? ResponseBody { get; } = responseBody;
}
