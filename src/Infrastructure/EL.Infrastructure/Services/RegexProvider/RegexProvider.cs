using System.Text.RegularExpressions;
using EL.Domain.Frontend.Lexer.Services.RegexProvider;

namespace EL.Infrastructure.Services.RegexProvider;

/// <inheritdoc cref="IRegexProvider" />
internal partial class RegexProvider : IRegexProvider
{
    /// <inheritdoc cref="IRegexProvider.Regex" />
    [GeneratedRegex(PatternContainer.Value)]
    public partial Regex Regex { get; }
}