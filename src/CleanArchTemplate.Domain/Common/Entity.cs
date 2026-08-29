namespace CleanArchTemplate.Domain.Common;

/// <summary>
/// Base para entidades de dominio. Coloque aqui propriedades comuns como Id, CreatedAt e UpdatedAt.
/// Regras de negocio devem ficar nas entidades concretas, nao em services de infraestrutura.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
}
