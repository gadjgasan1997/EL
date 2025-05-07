namespace EL.Domain.IR.Symbols;

/// <summary>
/// Символ
/// </summary>
/// <param name="name">Название</param>
public abstract class Symbol(string name) : ISymbol
{
    /// <inheritdoc cref="ISymbol.Id" />
    public abstract ISymbolId<ISymbol> Id { get; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; } = name;
}

/// <summary>
/// Комперер символа по его названию
/// </summary>
public class SymbolByNameComparer : IEqualityComparer<Symbol>
{
    /// <summary>
    /// Экзмепляр
    /// </summary>
    public static SymbolByNameComparer Instance { get; } = new();
    
    public bool Equals(Symbol? x, Symbol? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        return y is not null && x.Id.Equals(y.Id);
    }
    
    public int GetHashCode(Symbol obj) => obj.Name.GetHashCode();
}