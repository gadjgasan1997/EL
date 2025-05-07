namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id пространства имен
/// </summary>
public class NamespaceSymbolId : SymbolId<NamespaceSymbol>
{
    public NamespaceSymbolId(string name)
    {
        EqualityComponents = [name];
    }
    
    /// <inheritdoc cref="SymbolId{T}.EqualityComponents" />
    protected override IEnumerable<object?> EqualityComponents { get; }
}