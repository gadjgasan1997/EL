using EL.Domain.IR;
using EL.Domain.Frontend.Parser;

namespace EL.Application.StaticAnalysis.Services.ScopesResolver;

/// <summary>
/// Сервис определения корректной области видимости
/// </summary>
internal interface IScopesResolver
{
    /// <summary>
    /// Определяет область видимости
    /// </summary>
    /// <param name="symbolId">Id символа</param>
    /// <returns>Область видимости</returns>
    Scope Resolve<TSymbol>(ISymbolId<TSymbol> symbolId)
        where TSymbol : class, ISymbol;
}