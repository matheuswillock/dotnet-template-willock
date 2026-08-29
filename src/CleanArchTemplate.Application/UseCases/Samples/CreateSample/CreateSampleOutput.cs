using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.UseCases.Samples.CreateSample;

/// <summary>
/// Output especifico do use case. A controller deve retornar este conteudo quando IOutput.IsValid for true.
/// </summary>
public sealed record CreateSampleOutput(Guid Id, string Name)
{
    public static implicit operator CreateSampleOutput(SampleEntity entity) => new(entity.Id, entity.Name);
}
