namespace EL.Domain.IR.Symbols.Ids;

/// <summary>
/// Id символа
/// </summary>
/// <typeparam name="TSymbol">Тип символа</typeparam>
public abstract class SymbolId<TSymbol> : ISymbolId<TSymbol>
    where TSymbol : class, ISymbol
{
    /// <summary>
    /// Компоненты для сравнения символа на равенство
    /// </summary>
    protected abstract IEnumerable<object?> EqualityComponents { get; }
    
    public bool Equals(ISymbolId<ISymbol>? other) =>
        other is SymbolId<TSymbol> id && EqualityComponents.SequenceEqual(id.EqualityComponents);
    
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((SymbolId<TSymbol>) obj);
    }
    
    public override int GetHashCode()
    {
        return EqualityComponents
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
    
    public static bool operator ==(SymbolId<TSymbol>? left, SymbolId<TSymbol>? right) =>
        Equals(left, right);
    
    public static bool operator !=(SymbolId<TSymbol>? left, SymbolId<TSymbol>? right) =>
        !Equals(left, right);
}