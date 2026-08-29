using CleanArchTemplate.Application.Common.Outputs;

namespace CleanArchTemplate.Application.UseCases.Samples.CreateSample;

/// <summary>
/// Contrato do use case. Controllers/endpoints dependem deste contrato, nao da implementacao concreta.
/// </summary>
public interface ICreateSampleUseCase
{
    Task<IOutput<CreateSampleOutput>> ExecuteAsync(CreateSampleInput input, CancellationToken cancellationToken = default);
}
