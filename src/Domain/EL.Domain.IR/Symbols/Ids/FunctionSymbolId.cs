using EL.Domain.Share.Dictionaries;

namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id функции
/// </summary>
public class FunctionSymbolId : SymbolId<FunctionSymbol>
{
    public FunctionSymbolId(string name, IEnumerable<ElType> parameters)
    {
        EqualityComponents = new List<object> { name }.Concat(parameters);
    }
    
    /// <inheritdoc cref="SymbolId{T}.EqualityComponents" />
    protected override IEnumerable<object?> EqualityComponents { get; }
}