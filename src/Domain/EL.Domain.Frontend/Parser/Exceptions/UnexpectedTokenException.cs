using EL.Domain.Frontend.Lexer;

namespace EL.Domain.Frontend.Parser.Exceptions;

public class UnexpectedTokenException : ParserException
{
    public UnexpectedTokenException(Token token)
        : base($"Unexpected token '{token}'.")
    { }
}