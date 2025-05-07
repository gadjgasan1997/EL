namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id класса
/// </summary>
public class ClassSymbolId : SymbolId<ClassSymbol>
{
    public ClassSymbolId(string name)
    {
        EqualityComponents = [name];
    }
    
    /// <inheritdoc cref="SymbolId{T}.EqualityComponents" />
    protected override IEnumerable<object?> EqualityComponents { get; }
}