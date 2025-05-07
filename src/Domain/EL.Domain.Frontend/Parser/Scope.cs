namespace EL.Domain.Frontend.Parser;

/// <summary>
/// Область видимости
/// </summary>
public record Scope
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
    
    public override string ToString() => Id.ToString();
}