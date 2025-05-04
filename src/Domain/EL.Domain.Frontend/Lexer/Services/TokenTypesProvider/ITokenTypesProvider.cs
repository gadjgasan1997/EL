using EL.Domain.Share.SeedWork;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Lexer.Services.TokenTypesProvider;

/// <summary>
/// Провайдер получения токенов
/// </summary>
internal interface ITokenTypesProvider : IEnumerable<TokenType>, ISingletonService
{
    
}