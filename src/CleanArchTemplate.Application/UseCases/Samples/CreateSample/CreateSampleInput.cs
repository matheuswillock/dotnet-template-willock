using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.UseCases.Samples.CreateSample;

/// <summary>
/// Input recebido pelo use case. Controllers/endpoints devem converter requests HTTP para este tipo.
/// Use implicit operator ou metodos estaticos quando precisar transformar input em entidade de dominio.
/// </summary>
public sealed record CreateSampleInput(string Name)
{
    public static implicit operator SampleEntity(CreateSampleInput input) => new(input.Name);
}
