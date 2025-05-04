using static EL.Domain.Share.Dictionaries.TokenType;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Типы токенов
/// </summary>
public static class TokenTypes
{
    public static TokenType[] ElLiteralsTokens =>
    [
        DoubleLiteral,
        IntLiteral,
        BooleanLiteral,
        NullLiteral,
        StringLiteral
    ];
    
    public static TokenType[] ElAllTypesTokens =>
    [
        VarType,
        VoidType,
        ..ElConcreteTypesTokens
    ];
    
    public static TokenType[] ElConcreteTypesTokens =>
    [
        IntType,
        LongType,
        DoubleType,
        BooleanType,
        StringType
    ];
    
    /// <summary>
    /// Поток
    /// </summary>
    public static IEnumerable<TokenType> Stream
    {
        get
        {
            yield return Comment;
            yield return DoubleLiteral;
            yield return IntLiteral;
            yield return BooleanLiteral;
            yield return NullLiteral;
            yield return StringLiteral;
            yield return TokenType.Keyword;
            yield return VoidType;
            yield return VarType;
            yield return IntType;
            yield return LongType;
            yield return DoubleType;
            yield return BooleanType;
            yield return StringType;
            yield return TokenType.Attribute;
            yield return Identifier;
            yield return TokenType.Operator;
            yield return Question;
            yield return Colon;
            yield return Semicolon;
            yield return Assign;
            yield return Comma;
            yield return LeftCurl;
            yield return RightCurl;
            yield return LeftParen;
            yield return RightParen;
        }
    }
}