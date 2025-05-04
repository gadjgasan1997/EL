using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Lexer.CodeCoordinates;

namespace EL.Domain.Frontend.Lexer;

/// <summary>
/// Токен
/// </summary>
/// <param name="Type">Тип</param>
/// <param name="Segment">Сегмент</param>
/// <param name="Value">Значение</param>
public record Token(TokenType Type, Segment Segment, string Value)
{
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => $"{Type} {Segment}: {Value}";
}