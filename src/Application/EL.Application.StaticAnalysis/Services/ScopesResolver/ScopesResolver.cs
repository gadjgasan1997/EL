using EL.Domain.IR;
using EL.Domain.Frontend.Parser;
using System.Collections.Concurrent;

namespace EL.Application.StaticAnalysis.Services.ScopesResolver;

/// <inheritdoc cref="IScopesResolver" />
internal class ScopesResolver : IScopesResolver
{
    private static readonly ConcurrentDictionary<ISymbolId, Lazy<Scope>> _dictionary = [];
    
    /// <inheritdoc cref="IScopesResolver.Resolve{TSymbol}" />
    public Scope Resolve<TSymbol>(ISymbolId<TSymbol> symbolId)
        where TSymbol : class, ISymbol
    {
        return _dictionary.GetOrAdd(symbolId, _ => new Lazy<Scope>(() => new Scope())).Value;
    }
}