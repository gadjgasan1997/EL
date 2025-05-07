namespace EL.Domain.IR.SymbolTable;

/// <summary>
/// Таблица символов
/// </summary>
public interface ISymbolTable
{
    /// <summary>
    /// Родительская таблица
    /// </summary>
    ISymbolTable? Parent { get; }
    
    /// <summary>
    /// Добавляет символ
    /// </summary>
    /// <param name="symbol">Символ</param>
    void AddSymbol(ISymbol symbol);
    
    /// <summary>
    /// Возвращает сивол
    /// </summary>
    /// <param name="id">Id</param>
    /// <typeparam name="TSymbol">Тип символа</typeparam>
    /// <returns>Символ</returns>
    public TSymbol? FindSymbol<TSymbol>(ISymbolId<TSymbol> id)
        where TSymbol : class, ISymbol;
    
    /// <summary>
    /// Возвращает признак, присутствует ли символ с указанным Id в таблице
    /// </summary>
    /// <param name="id">Id символа</param>
    /// <returns>Признак</returns>
    bool ContainsSymbol(ISymbolId<ISymbol> id);
}