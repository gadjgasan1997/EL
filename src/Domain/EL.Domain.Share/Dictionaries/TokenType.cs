using EL.Domain.Share.SeedWork;
using static EL.Domain.Share.Dictionaries.ElType;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Тип токена
/// </summary>
public class TokenType : Enumeration
{
    /// <summary>
    /// Паттерн
    /// </summary>
    public string Pattern { get; }
    
    /// <summary>
    /// Приоритет
    /// </summary>
    public int Priority { get; }
    
    /// <summary>
    /// Признак, может ли токен быть проигнорирован
    /// </summary>
    public bool CanIgnore { get; }
    
    public static TokenType Comment { get; } = new(
        value: "Comment",
        pattern: "[/]{2}.*",
        priority: 0,
        canIgnore: true);
    
    public static TokenType DoubleLiteral { get; } = new(
        value: "DoubleLiteral",
        pattern: "[0-9]+[.][0-9]+",
        priority: 10);
    
    public static TokenType IntLiteral { get; } = new(
        value: "IntLiteral",
        pattern: "[0-9]+",
        priority: 20);
    
    public static TokenType BooleanLiteral { get; } = new(
        value: "BooleanLiteral",
        pattern: "(?<![a-zA-Z0-9])(true|false)(?![a-zA-Z0-9])",
        priority: 30);
    
    public static TokenType NullLiteral { get; } = new(
        value: "NullLiteral",
        pattern: "(?<![a-zA-Z0-9])(null)(?![a-zA-Z0-9])",
        priority: 40);
    
    public static TokenType StringLiteral { get; } = new(
        value: "StringLiteral",
        pattern: """
                 \"(\\.|[^"\\])*\"
                 """,
        priority: 50);
    
    public static TokenType Keyword { get; } = new(
        value: "Keyword",
        pattern: "(?<![a-zA-Z0-9])(namespace|class|if|else|while|for|break|continue|return)(?![a-zA-Z0-9])",
        priority: 60);
    
    public static TokenType VoidType { get; } = new(
        value: VOID_TYPE,
        pattern: "(?<![a-zA-Z0-9])(void)(?![a-zA-Z0-9])",
        priority: 61);
    
    public static TokenType VarType { get; } = new(
        value: VAR_TYPE,
        pattern: "(?<![a-zA-Z0-9])(var)(?![a-zA-Z0-9])",
        priority: 62);
    
    public static TokenType IntType { get; } = new(
        value: INT_TYPE,
        pattern: "(?<![a-zA-Z0-9])(int)(?![a-zA-Z0-9])",
        priority: 63);
    
    public static TokenType LongType { get; } = new(
        value: LONG_TYPE,
        pattern: "(?<![a-zA-Z0-9])(long)(?![a-zA-Z0-9])",
        priority: 64);
    
    public static TokenType DoubleType { get; } = new(
        value: DOUBLE_TYPE,
        pattern: "(?<![a-zA-Z0-9])(double)(?![a-zA-Z0-9])",
        priority: 65);
    
    public static TokenType BooleanType { get; } = new(
        value: BOOLEAN_TYPE,
        pattern: "(?<![a-zA-Z0-9])(bool)(?![a-zA-Z0-9])",
        priority: 66);
    
    public static TokenType StringType { get; } = new(
        value: STRING_TYPE,
        pattern: "(?<![a-zA-Z0-9])(string)(?![a-zA-Z0-9])",
        priority: 67);
    
    public static TokenType Attribute { get; } = new(
        value: "Attribute",
        pattern: ".*@[A-Za-z0-9]*",
        priority: 70);
    
    public static TokenType Identifier { get; } = new(
        value: "Identifier",
        pattern: "[a-zA-Z][a-zA-Z0-9]*",
        priority: 80);
    
    public static TokenType Operator { get; } = new(
        value: "Operator",
        pattern: "[+]{1,2}|[-]{1,2}|[*]|[/]|[%]|([!]|[=])[=]|([<]|[>])[=]?|[!]|[?]{2}|[|]{2}|[&]{2}|[~]",
        priority: 90);
    
    public static TokenType Question { get; } = new(
        value: "Question",
        pattern: "[?]",
        priority: 100);
    
    public static TokenType Colon { get; } = new(
        value: "Colon",
        pattern: "[:]",
        priority: 110);
    
    public static TokenType Semicolon { get; } = new(
        value: "SemiColon",
        pattern: "[;]",
        priority: 120);
    
    public static TokenType Assign { get; } = new(
        value: "Assign",
        pattern: "[=]",
        priority: 130);
    
    public static TokenType Comma { get; } = new(
        value: "Comma",
        pattern: "[,]",
        priority: 140);
    
    public static TokenType LeftCurl { get; } = new(
        value: "LeftCurl",
        pattern: "[{]",
        priority: 150);
    
    public static TokenType RightCurl { get; } = new(
        value: "RightCurl",
        pattern: "[}]",
        priority: 160);
    
    public static TokenType LeftParen { get; } = new(
        value: "LeftParen",
        pattern: "[(]",
        priority: 170);
    
    public static TokenType RightParen { get; } = new(
        value: "RightParen",
        pattern: "[)]",
        priority: 180);
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => Value;
    
    private TokenType(
        string value,
        string pattern,
        int priority,
        bool canIgnore = false)
        : base(value)
    {
        Pattern = pattern;
        Priority = priority;
        CanIgnore = canIgnore;
    }
}