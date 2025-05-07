using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.IR.Symbols;

/// <summary>
/// Параметр функции
/// </summary>
public class FunctionParameterSymbol : Symbol
{
    /// <summary>
    /// Тип
    /// </summary>
    public ElType Type { get; }
    
    public FunctionParameterSymbol(ElType type, string name)
        : base(name)
    {
        Type = type;
        Id = new FunctionParameterSymbolId(name);
    }
    
    /// <inheritdoc cref="Symbol.Id" />
    public override ISymbolId<ISymbol> Id { get; }
}