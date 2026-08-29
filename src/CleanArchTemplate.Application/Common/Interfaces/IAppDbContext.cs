using CleanArchTemplate.Domain.Entities;

namespace CleanArchTemplate.Application.Common.Interfaces;

/// <summary>
/// Contrato do DbContext usado pela Application. A implementacao concreta fica na Infrastructure.
/// Adicione DbSets aqui quando criar novas entidades persistidas.
/// </summary>
public interface IAppDbContext
{
    IQueryable<SampleEntity> SampleEntities { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
