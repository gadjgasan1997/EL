using EL.Domain.Frontend.Lexer;
using EL.Domain.Frontend.Lexer.CodeCoordinates;
using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Parser.Exceptions;

[Serializable]
public class ParserException : Exception
{
    public ParserException() { }
    
    public ParserException(string message) : base(message) { }
    
    protected ParserException(string message, Exception inner) : base(message, inner) { }
    
    public ParserException(Segment segment, string? expected, Token actual) :
        base($"Wrong syntax: {segment} expected {expected}. Actual = ({actual.Type.Value}, {actual.Value})")
    { }
    
    public ParserException(Segment segment, TokenType[] expected) :
        base($"Wrong syntax: {segment} expected one of {string.Join(", ", expected.Select(type => type.Value))}.")
    { }
}