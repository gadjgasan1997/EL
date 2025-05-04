using System.Collections;
using System.Collections.Frozen;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Lexer.Services.TokenTypesProvider;

/// <inheritdoc cref="ITokenTypesProvider" />
internal class TokenTypesProvider : ITokenTypesProvider
{
    private static readonly FrozenDictionary<string, TokenType> _types = TokenTypes.Stream
        .OrderBy(type => type.Priority)
        .ToFrozenDictionary(type => type.Value);
    
    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
    public IEnumerator<TokenType> GetEnumerator() => _types.Select(pair => pair.Value).GetEnumerator();
    
    /// <inheritdoc cref="IEnumerable.GetEnumerator" />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}