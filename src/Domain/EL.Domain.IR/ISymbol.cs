namespace EL.Domain.IR;

/// <summary>
/// Символ
/// </summary>
public interface ISymbol
{
    /// <summary>
    /// Id
    /// </summary>
    public ISymbolId<ISymbol> Id { get; }
}