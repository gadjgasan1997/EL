namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id параметра функции
/// </summary>
public class FunctionParameterSymbolId : SymbolId<FunctionParameterSymbol>
{
    public FunctionParameterSymbolId(string name)
    {
        EqualityComponents = [name];
    }
    
    /// <inheritdoc cref="SymbolId{T}.EqualityComponents" />
    protected override IEnumerable<object?> EqualityComponents { get; }
}