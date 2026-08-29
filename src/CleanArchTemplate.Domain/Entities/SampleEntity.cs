using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Domain.Entities;

/// <summary>
/// Exemplo de entidade. Substitua por entidades reais do seu dominio.
/// A entidade deve proteger invariantes e expor metodos com nomes de negocio.
/// </summary>
public sealed class SampleEntity : Entity
{
    private SampleEntity() { }

    public SampleEntity(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Name = name.Trim();
    }

    public string Name { get; private set; } = string.Empty;

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Name = name.Trim();
        MarkAsUpdated();
    }
}
