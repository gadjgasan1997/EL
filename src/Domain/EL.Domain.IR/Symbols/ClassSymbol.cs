using EL.Domain.IR.Symbols.Ids;

namespace EL.Domain.IR.Symbols;

/// <summary>
/// Класс
/// </summary>
public class ClassSymbol : Symbol
{
    public ClassSymbol(string name)
        : base(name)
    {
        Id = new ClassSymbolId(name);
    }
    
    /// <inheritdoc cref="Symbol.Id" />
    public override ISymbolId<ISymbol> Id { get; }
}