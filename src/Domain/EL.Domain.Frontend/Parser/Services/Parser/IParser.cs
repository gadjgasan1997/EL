using EL.Domain.Share.SeedWork;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Lexer.TokensEnumerator;

namespace EL.Domain.Frontend.Parser.Services.Parser;

/// <summary>
/// Парсер
/// </summary>
public interface IParser : ITransientService
{
    /// <summary>
    /// Парсит список токенов в АСТ
    /// </summary>
    /// <param name="tokens">Токены</param>
    /// <returns>АСТ</returns>
    IAbstractSyntaxTree Parse(ITokensEnumerator tokens);
}