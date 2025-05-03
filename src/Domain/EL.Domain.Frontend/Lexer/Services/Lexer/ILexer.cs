using EL.Domain.Share.SeedWork;
using EL.Domain.Frontend.Lexer.TokensEnumerator;

namespace EL.Domain.Frontend.Lexer.Services.Lexer;

/// <summary>
/// Лексер
/// </summary>
public interface ILexer : IDomainService
{
    /// <summary>
    /// Возвращает список токенов
    /// </summary>
    /// <param name="text">Текст</param>
    /// <returns>Список токенов</returns>
    public ITokensEnumerator GetTokens(string text);
}