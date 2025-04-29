using EL.Domain.Share.SeedWork;
using EL.Domain.Frontend.Lexer;
using EL.Domain.Frontend.Parser.Ast;

namespace EL.Domain.Frontend.Parser.Services.Parser;

/// <summary>
/// Парсер
/// </summary>
public interface IParser : IDomainService
{
    /// <summary>
    /// Парсит список токенов в АСТ
    /// </summary>
    /// <param name="tokens">Список токенов</param>
    /// <returns>АСТ</returns>
    IAbstractSyntaxTree Parse(IReadOnlyCollection<Token> tokens);
}