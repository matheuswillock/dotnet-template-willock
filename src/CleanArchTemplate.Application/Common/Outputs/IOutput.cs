namespace CleanArchTemplate.Application.Common.Outputs;

/// <summary>
/// Padrao de comunicacao entre Controllers/Endpoints e Use Cases.
/// Use cases retornam IOutput para a API decidir o HTTP status sem conhecer detalhes internos do caso de uso.
/// </summary>
public interface IOutput<T>
{
    bool IsValid { get; }
    List<string> SuccessMessages { get; }
    List<string> ErrorMessages { get; }
    T? Result { get; }

    T GetResult();
    void AddResult(T? result, string? successMessage = null);
    void AddErrorMessage(string error);
    void AddSuccessMessage(string successMessage);
    void AddErrorMessages(List<string> errors);
    void AddSuccessMessages(List<string> successMessages);
    void AddErrorMessages(params string[] errors);
    void AddSuccessMessages(params string[] successMessages);
    void Clear();
    List<string> GetErrorMessages();
    List<string> GetSuccessMessages();
    bool HasErrors();
    bool HasSuccessMessages();
    bool HasResult();
}
