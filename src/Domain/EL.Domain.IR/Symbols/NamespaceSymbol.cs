using EL.Domain.IR.Symbols.Ids;

namespace EL.Domain.IR.Symbols;

/// <summary>
/// Пространство имен
/// </summary>
public class NamespaceSymbol : Symbol
{
    public NamespaceSymbol(string name)
        : base(name)
    {
        Id = new NamespaceSymbolId(name);
    }
    
    /// <inheritdoc cref="Symbol.Id" />
    public override ISymbolId<ISymbol> Id { get; }
}