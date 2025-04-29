using System.Collections;
using EL.Domain.Frontend.Lexer;

namespace EL.Domain.Frontend.Parser.Services.Parser;

/// <summary>
/// Поток токенов
/// </summary>
internal class TokensStream : IEnumerator<Token>
{
    private readonly IEnumerator<Token> _inner;
    
    private TokensStream(IEnumerator<Token> enumerator)
    {
        _inner = enumerator;
        _inner.MoveNext();
    }
    
    /// <inheritdoc cref="IEnumerator{T}.Current" />
    public Token Current => _inner.Current;
    
    /// <inheritdoc cref="IEnumerator.Current" />
    object IEnumerator.Current => Current;
    
    /// <inheritdoc cref="IEnumerator.MoveNext" />
    public bool MoveNext() => _inner.MoveNext();
    
    /// <inheritdoc cref="IEnumerator.Reset" />
    public void Reset() => _inner.Reset();
    
    /// <inheritdoc cref="IDisposable.Dispose" />
    public void Dispose() => _inner.Dispose();
    
    public static implicit operator TokensStream(List<Token> tokens) =>
        new (tokens.GetEnumerator());
}