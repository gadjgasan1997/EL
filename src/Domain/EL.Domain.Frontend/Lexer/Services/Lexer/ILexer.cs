using EL.Domain.Share.SeedWork;

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
    public IReadOnlyCollection<Token> GetTokens(string text);
}