using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.IR.Symbols;

/// <summary>
/// Функция
/// </summary>
public class FunctionSymbol : Symbol
{
    public FunctionSymbol(string name, IEnumerable<FunctionParameterSymbol> parameters)
        : base(name)
    {
        Parameters = parameters.ToList();
        Id = new FunctionSymbolId(name, Parameters.Select(symbol => symbol.Type));
    }
    
    /// <summary>
    /// Параметры
    /// </summary>
    public IReadOnlyList<FunctionParameterSymbol> Parameters { get; }
    
    /// <inheritdoc cref="Symbol.Id" />
    public override ISymbolId<ISymbol> Id { get; }
}