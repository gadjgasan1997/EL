namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id переменной
/// </summary>
public class VariableSymbolId : SymbolId<VariableSymbol>
{
    public VariableSymbolId(string name)
    {
        EqualityComponents = [name];
    }
    
    /// <inheritdoc cref="SymbolId{T}.EqualityComponents" />
    protected override IEnumerable<object?> EqualityComponents { get; }
}