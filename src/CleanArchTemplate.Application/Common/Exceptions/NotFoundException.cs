namespace CleanArchTemplate.Application.Common.Exceptions;

/// <summary>
/// Use quando um recurso esperado nao existir. A API converte para HTTP 404 no middleware global.
/// </summary>
public sealed class NotFoundException(string message) : Exception(message);
