using System.Text.RegularExpressions;
using EL.Domain.Frontend.Lexer.CodeCoordinates;
using EL.Domain.Frontend.Lexer.Services.RegexProvider;
using EL.Domain.Frontend.Lexer.Services.TokenTypesProvider;
using EL.Domain.Frontend.Lexer.TokensEnumerator;

namespace EL.Domain.Frontend.Lexer.Services.Lexer;

/// <inheritdoc cref="ILexer" />
internal class RegexLexer(
    ICoordinatesService coordinatesService,
    IRegexProvider regexProvider,
    ITokenTypesProvider tokenTypesProvider) : ILexer
{
    /// <inheritdoc cref="ILexer.GetTokens" />
    public ITokensEnumerator GetTokens(string text) =>
        new TokensEnumerator.TokensEnumerator(
            GetTokensCore(text).Where(token => !token.Type.CanIgnore).ToList());
    
    private IEnumerable<Token> GetTokensCore(string text)
    {
        var lines = coordinatesService.GetLines(text);
        foreach (Match match in regexProvider.Regex.Matches(text))
        {
            foreach (var type in tokenTypesProvider)
            {
                var group = match.Groups[type.Value];
                if (!group.Success) continue;
                
                var segment = new Segment(
                    coordinatesService.GetCoordinate(group.Index, lines),
                    coordinatesService.GetCoordinate(absoluteIndex: group.Index + group.Length, lines));
                
                var token = new Token(type, segment, group.Value);
                yield return token;
            }
        }
    }
}