using System.Collections;
using EL.CommonUtils.Extensions;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Exceptions;
using EL.Domain.Frontend.Lexer.Services.TokenTypesProvider;
using static EL.Domain.Share.Dictionaries.TokenTypes;

namespace EL.Domain.Frontend.Lexer.TokensEnumerator;

/// <inheritdoc cref="ITokensEnumerator" />
internal class TokensEnumerator : ITokensEnumerator
{
#pragma warning disable CA1859
    private readonly IEnumerator<Token> _enumerator;
#pragma warning restore CA1859
    
    private readonly ITokenTypesProvider _provider;
    
    public TokensEnumerator(ITokenTypesProvider provider, List<Token> tokens)
    {
        _provider = provider;
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
    public Token Expect(string tag, string? value = null)
    {
        var current = CheckCurrentNotNull(tag, value);
        
        if (!CurrentIs(tag))
            throw new ParserException(_enumerator.Current.Segment, tag, _enumerator.Current);
        
        if (_enumerator.Current.Value != (value ?? _enumerator.Current.Value))
            throw new ParserException(_enumerator.Current.Segment, value, _enumerator.Current);
        
        _enumerator.MoveNext();
        return current;
    }
    
    /// <inheritdoc cref="ITokensEnumerator.Expect" />
    public Token ExpectAny(string[] tags)
    {
        foreach (var tag in tags)
        {
            var current = CheckCurrentNotNull(tag);
            if (!CurrentIs(tag))
                continue;
            
            _enumerator.MoveNext();
            return current;
        }
        
        throw new ParserException(_enumerator.Current.Segment, tags);
    }
    
    /// <inheritdoc cref="ITokensEnumerator.ExpectOperator" />
    public Token ExpectOperator(Operator? @operator = null) => Expect(OperatorTag, @operator?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.ExpectKeyword" />
    public Token ExpectKeyword(Keyword keyword) => Expect(KeywordTag, keyword.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIs" />
    public bool CurrentIs(string tag, string? value = null)
    {
        var current = CheckCurrentNotNull(tag, value);
        return current.Type == _provider.GetTokenTypeByTag(tag) &&
               (value is null || value == current.Value);
    }
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsAny" />
    public bool CurrentIsAny(string[] tags) => tags.Any(tag => CurrentIs(tag));
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsOperator" />
    public bool CurrentIsOperator(Operator? @operator = null) => CurrentIs(OperatorTag, @operator?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.CurrentIsKeyword" />
    public bool CurrentIsKeyword(Keyword? keyword = null) => CurrentIs(KeywordTag, keyword?.Value);
    
    /// <inheritdoc cref="ITokensEnumerator.ForCurrent{T}" />
    public T ForCurrent<T>(params (string, Func<string, T>)[] tuples)
    {
        foreach (var (tag, func) in tuples)
        {
            if (CurrentIs(tag))
                return func(tag);
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
    
    private Token CheckCurrentNotNull(string tag, string? value = null)
    {
        _enumerator.Current.CheckNotNull(
            errorMessage: "Неожиданный конец строки. Ожидался тег: " +
                          $"'{tag}{(value is null ? string.Empty : $"-{value}")}'",
            parameterName: nameof(tag));
        
        return _enumerator.Current;
    }
}