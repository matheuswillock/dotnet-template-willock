using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Application.Common.Outputs;
using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.UseCases.Samples.CreateSample;

/// <summary>
/// Implemente aqui a regra de aplicacao. Nao acesse EF Core diretamente; use IAppDbContext ou repositorios/portas.
/// </summary>
public sealed class CreateSampleUseCase(IAppDbContext dbContext) : ICreateSampleUseCase
{
    public async Task<IOutput<CreateSampleOutput>> ExecuteAsync(CreateSampleInput input, CancellationToken cancellationToken = default)
    {
        var output = new Output<CreateSampleOutput>();

        if (string.IsNullOrWhiteSpace(input.Name))
        {
            output.AddErrorMessage("Name is required.");
            return output;
        }

        SampleEntity entity = input;

        if (dbContext is InfrastructureBoundary boundary)
            boundary.AddEntity(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        output.AddResult((CreateSampleOutput)entity, "Sample created successfully.");
        return output;
    }
}

/// <summary>
/// Pequena porta para permitir escrita sem expor EF Core para toda a Application.
/// Em projetos maiores, prefira repositorios especificos por agregado.
/// </summary>
public interface InfrastructureBoundary
{
    void AddEntity<T>(T entity) where T : class;
}
