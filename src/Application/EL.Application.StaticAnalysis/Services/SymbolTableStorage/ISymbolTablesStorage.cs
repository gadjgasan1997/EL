using EL.Domain.IR.SymbolTable;
using EL.Domain.Frontend.Parser;

namespace EL.Application.StaticAnalysis.Services.SymbolTableStorage;

/// <summary>
/// Хранилище таблиц символов
/// </summary>
internal interface ISymbolTablesStorage
{
    /// <summary>
    /// Возвращает таблицу символов по области видимости
    /// </summary>
    /// <param name="scope">Область видимости</param>
    ISymbolTable this[Scope scope] { get; }
    
    /// <summary>
    /// Возвращает необязательную таблицу символов
    /// </summary>
    /// <param name="scope">Область видимости</param>
    /// <returns>Таблица символов</returns>
    ISymbolTable? GetOptionTable(Scope scope);
    
    /// <summary>
    /// Инициализирует таблицу символов для области видимости
    /// </summary>
    /// <param name="scope">Область видимости</param>
    /// <param name="symbolTable">Таблица символов</param>
    void Init(Scope scope, ISymbolTable symbolTable);
}