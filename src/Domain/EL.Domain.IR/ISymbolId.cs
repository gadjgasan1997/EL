namespace EL.Domain.IR;

/// <summary>
/// Id символа
/// </summary>
public interface ISymbolId;

/// <summary>
/// Id символа
/// </summary>
/// <typeparam name="TSymbol">Тип символа</typeparam>
public interface ISymbolId<out TSymbol> : ISymbolId, IEquatable<ISymbolId<ISymbol>>
    where TSymbol : class, ISymbol;