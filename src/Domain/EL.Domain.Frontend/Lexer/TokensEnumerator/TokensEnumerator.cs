using System.Collections;
using EL.CommonUtils.Extensions;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Exceptions;

namespace EL.Domain.Frontend.Lexer.TokensEnumerator;

/// <inheritdoc cref="ITokensEnumerator" />
internal class TokensEnumerator : ITokensEnumerator
{
#pragma warning disable CA1859
    private readonly IEnumerator<Token> _enumerator;
#pragma warning restore CA1859
    
    public TokensEnumerator(List<Token> tokens)
    {
        _enumerator = tokens.GetEnumerator();
        _enumerator.MoveNext();
    }
    
    /// <inheritdoc cref="IEnumerator.MoveNext" />
    public bool MoveNext() => _enumerator.MoveNext();
    
    /// <inheritdoc cref="IEnumerator.Reset" />
    public void Reset() => _enumerator.Reset();
    
    /// <inheritdoc cref="IEnumerator.Current" />
    object IEnumerator.Current => Current;
    
    /// <inheritdoc cref="IEnumerator{T}.Current" />
    public Token Current => _enumerator.Current;
    
    /// <inheritdoc cref="IDisposable.Dispose" />
    public void Dispose() => _enumerator.Dispose();
    
    /// <inheritdoc cref="ITokensEnumerator.Expect" />
    public Token Expect(TokenType type, string? value = null)
    {
        var current = CheckCurrentNotNull(type, value);
        
        if (!CurrentIs(type))
            throw new ParserException(_enumerator.Current.Segment, type.Value, _enumerator.Current);
        
        if (_enumerator.Current.Value != (value ?? _enumerator.Current.Value))
            throw new ParserException(_enumerator.Current.Segment, value, _enumerator.Current);
        
        _enumerator.MoveNext();
        return current;
    }
    
    /// <inheritdoc cref="ITokensEnumerator.Expect" />
    public Token ExpectAny(TokenType[] types)
    {
        foreach (var type in types)
        {
            var current = CheckCurrentNotNull(type);
            if (!CurrentIs(type))
                continue;
            
            _enumerator.MoveNext();
            return current;
        }
        
        throw new ParserException(_enumerator.Current.Segment, types);
    }
    
    /// <inheritdoc cref="ITokensEnumerator.ExpectOperator" />
    public Token ExpectOperator(Operator? @operator = null) => Expect(TokenType.Operator, @operator?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.ExpectKeyword" />
    public Token ExpectKeyword(Keyword keyword) => Expect(TokenType.Keyword, keyword.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIs" />
    public bool CurrentIs(TokenType type, string? value = null)
    {
        var current = CheckCurrentNotNull(type, value);
        return current.Type == type && (value is null || value == current.Value);
    }
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsAny" />
    public bool CurrentIsAny(TokenType[] types) => types.Any(type => CurrentIs(type));
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsOperator" />
    public bool CurrentIsOperator(Operator? @operator = null) => CurrentIs(TokenType.Operator, @operator?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsKeyword" />
    public bool CurrentIsKeyword(Keyword? keyword = null) => CurrentIs(TokenType.Keyword, keyword?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.ForCurrent{T}" />
    public T ForCurrent<T>(params (TokenType, Func<TokenType, T>)[] tuples)
    {
        foreach (var (type, func) in tuples)
        {
            if (CurrentIs(type))
                return func(type);
        }
        
        throw new UnexpectedTokenException(_enumerator.Current);
    }
    
    /// <inheritdoc cref="ITokensEnumerator.ForCurrentKeyword{T}" />
    public T ForCurrentKeyword<T>(params (Keyword, Func<T>)[] tuples)
    {
        foreach (var (keyword, func) in tuples)
        {
            if (CurrentIsKeyword(keyword))
                return func();
        }
        
        throw new UnexpectedTokenException(_enumerator.Current);
    }
    
    private Token CheckCurrentNotNull(TokenType type, string? value = null)
    {
        _enumerator.Current.CheckNotNull(
            errorMessage: "Неожиданный конец строки. Ожидался тег: " +
                          $"'{type.Value}{(value is null ? string.Empty : $"-{value}")}'",
            parameterName: nameof(type));
        
        return _enumerator.Current;
    }
}