using EL.Domain.Share.SeedWork;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Lexer.Services.TokenTypesProvider;

/// <summary>
/// Провайдер получения токенов
/// </summary>
internal interface ITokenTypesProvider : IEnumerable<TokenTypes.TokenType>, IDomainService
{
    /// <summary>
    /// Возвращает тип токена по тегу
    /// </summary>
    /// <param name="tag">Тег</param>
    /// <returns>Тип токена</returns>
    TokenTypes.TokenType GetTokenTypeByTag(string tag);
}