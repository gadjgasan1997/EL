using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.IR.Symbols;

/// <summary>
/// Переменная
/// </summary>
public class VariableSymbol : Symbol
{
    /// <summary>
    /// Тип
    /// </summary>
    public ElType Type { get; }
    
    /// <summary>
    /// Признак, что переменная была проинициализирована
    /// </summary>
    public bool IsInitialized { get; set; }
    
    public VariableSymbol(ElType type, string name)
        : base(name)
    {
        Type = type;
        Id = new VariableSymbolId(name);
    }
    
    /// <inheritdoc cref="Symbol.Id" />
    public override ISymbolId<ISymbol> Id { get; }
}