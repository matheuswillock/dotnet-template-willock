using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Application.UseCases.Samples.CreateSample;
using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchTemplate.Infrastructure.Persistence;

/// <summary>
/// DbContext principal da aplicacao. Configure DbSets e mapeamentos EF Core aqui.
/// Troque o provider InMemory em DependencyInjection por PostgreSQL/SQL Server quando definir o banco real.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext, InfrastructureBoundary
{
    public DbSet<SampleEntity> Samples => Set<SampleEntity>();
    public IQueryable<SampleEntity> SampleEntities => Samples.AsNoTracking();

    public void AddEntity<T>(T entity) where T : class => Set<T>().Add(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
