using EL.CommonUtils.Extensions;
using EL.Domain.IR.SymbolTable;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Extensions;
using EL.Application.StaticAnalysis.Services.SymbolTableStorage;

namespace EL.Application.StaticAnalysis.Extensions;

/// <summary>
/// Методы расширения для <see cref="ISymbolTablesStorage" />
/// </summary>
internal static class SymbolTablesStorageExtensions
{
    /// <summary>
    /// Возвращает обязательную таблицу символов
    /// </summary>
    /// <param name="storage">Хранилище таблиц символов</param>
    /// <param name="node">Нода</param>
    /// <returns>Таблица символов</returns>
    public static ISymbolTable GetRequiredSymbolTable(
        this ISymbolTablesStorage storage,
        IAbstractSyntaxTreeNode node)
    {
        var scope = node.Scope.ShouldBeInitialized();
        return storage[scope];
    }
    
    /// <summary>
    /// Возвращает родительскую таблицу символов
    /// </summary>
    /// <param name="storage">Хранилище таблиц символов</param>
    /// <param name="node">Нода</param>
    /// <returns>Родительскя таблица символов</returns>
    public static ISymbolTable GetParentTable(
        this ISymbolTablesStorage storage,
        IAbstractSyntaxTreeNode node)
    {
        var parent = node.Parent.CheckNotNull();
        var parentScope = parent.Scope.ShouldBeInitialized();
        return storage[parentScope];
    }
}