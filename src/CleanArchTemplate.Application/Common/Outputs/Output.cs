namespace CleanArchTemplate.Application.Common.Outputs;

/// <summary>
/// Implementacao padrao do retorno dos Use Cases.
/// Centralize aqui mensagens de sucesso, erros de regra/aplicacao e resultado do caso de uso.
/// </summary>
public sealed class Output<T> : IOutput<T>
{
    public bool IsValid { get; private set; } = true;
    public List<string> SuccessMessages { get; private set; } = [];
    public List<string> ErrorMessages { get; private set; } = [];
    public T? Result { get; private set; }

    public void AddResult(T? result, string? successMessage = null)
    {
        Result = result ?? throw new ArgumentNullException(nameof(result), "Result cannot be null.");

        if (!string.IsNullOrWhiteSpace(successMessage))
            AddSuccessMessage(successMessage);
    }

    public T GetResult() => Result ?? throw new InvalidOperationException("Result is null.");

    public void AddErrorMessage(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException("Error message cannot be empty.", nameof(error));

        ErrorMessages.Add(error);
        IsValid = false;
    }

    public void AddSuccessMessage(string successMessage)
    {
        if (string.IsNullOrWhiteSpace(successMessage))
            throw new ArgumentException("Success message cannot be empty.", nameof(successMessage));

        SuccessMessages.Add(successMessage);
    }

    public void AddErrorMessages(List<string> errors) => AddErrorMessages(errors.ToArray());

    public void AddSuccessMessages(List<string> successMessages) => AddSuccessMessages(successMessages.ToArray());

    public void AddErrorMessages(params string[] errors)
    {
        if (errors.Length == 0)
            throw new ArgumentException("Errors cannot be empty.", nameof(errors));

        ErrorMessages.AddRange(errors.Where(error => !string.IsNullOrWhiteSpace(error)));
        IsValid = false;
    }

    public void AddSuccessMessages(params string[] successMessages)
    {
        if (successMessages.Length == 0)
            throw new ArgumentException("Success messages cannot be empty.", nameof(successMessages));

        SuccessMessages.AddRange(successMessages.Where(message => !string.IsNullOrWhiteSpace(message)));
    }

    public void Clear()
    {
        SuccessMessages.Clear();
        ErrorMessages.Clear();
        Result = default;
        IsValid = true;
    }

    public List<string> GetErrorMessages() => [.. ErrorMessages];

    public List<string> GetSuccessMessages() => [.. SuccessMessages];

    public bool HasErrors() => ErrorMessages.Count != 0;

    public bool HasSuccessMessages() => SuccessMessages.Count != 0;
    public bool HasResult() => Result is not null;
}
