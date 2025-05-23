using EL.Domain.Share.SeedWork;
using System.Text.RegularExpressions;

namespace EL.Domain.Frontend.Lexer.Services.RegexProvider;

/// <summary>
/// Провайдер получения регулярного выражения
/// </summary>
public interface IRegexProvider : ISingletonService
{
    /// <summary>
    /// Выражение
    /// </summary>
    Regex Regex { get; }
}