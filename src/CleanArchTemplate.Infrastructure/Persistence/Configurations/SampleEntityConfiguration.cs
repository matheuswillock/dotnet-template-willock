using CleanArchTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchTemplate.Infrastructure.Persistence.Configurations;

/// <summary>
/// Exemplo de mapeamento EF Core. Crie uma classe de configuration para cada entidade persistida.
/// </summary>
public sealed class SampleEntityConfiguration : IEntityTypeConfiguration<SampleEntity>
{
    public void Configure(EntityTypeBuilder<SampleEntity> builder)
    {
        builder.ToTable("samples");
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
        builder.Property(entity => entity.CreatedAt).IsRequired();
    }
}
