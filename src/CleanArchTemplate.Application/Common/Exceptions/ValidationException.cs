namespace CleanArchTemplate.Application.Common.Exceptions;

/// <summary>
/// Use para erros de validacao de input. A API converte para HTTP 400 no middleware global.
/// </summary>
public sealed class ValidationException(IEnumerable<string> errors) : Exception("Validation failed.")
{
    public IReadOnlyCollection<string> Errors { get; } = errors.ToArray();
}
