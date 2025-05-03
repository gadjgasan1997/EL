namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Типы токенов
/// </summary>
public static class TokenTypes
{
    public static string[] ElLiteralsTokensTags =>
    [
        DoubleLiteralTag,
        IntLiteralTag,
        BooleanLiteralTag,
        NullLiteralTag,
        StringLiteralTag
    ];
    
    public static string[] ElAllTypesTokensTags =>
    [
        VarTypeTag,
        VoidTypeTag,
        ..ElConcreteTypesTokensTags
    ];
    
    public static string[] ElConcreteTypesTokensTags =>
    [
        IntTypeTag,
        LongTypeTag,
        DoubleTypeTag,
        BooleanTypeTag,
        StringTypeTag
    ];
    
    public static string CommentTag => "Comment";
    public static string DoubleLiteralTag => "DoubleLiteral";
    public static string IntLiteralTag => "IntLiteral";
    public static string BooleanLiteralTag => "BooleanLiteral";
    public static string StringLiteralTag => "StringLiteral";
    public static string KeywordTag => "Keyword";
    public static string VoidTypeTag => "VoidType";
    public static string VarTypeTag => "VarType";
    public static string DoubleTypeTag => "DoubleType";
    public static string IntTypeTag => "IntType";
    public static string LongTypeTag => "LongType";
    public static string BooleanTypeTag => "BooleanType";
    public static string NullLiteralTag => "NullLiteral";
    public static string StringTypeTag => "StringType";
    public static string AttributeTag => "Attribute";
    public static string IdentifierTag => "Identifier";
    public static string OperatorTag => "Operator";
    public static string QuestionTag => "Question";
    public static string ColonTag => "Colon";
    public static string SemicolonTag => "SemiColon";
    public static string AssignTag => "Assign";
    public static string CommaTag => "Comma";
    public static string LeftCurlTag => "LeftCurl";
    public static string RightCurlTag => "RightCurl";
    public static string LeftParenTag => "LeftParen";
    public static string RightParenTag => "RightParen";
    
    /// <summary>
    /// Поток
    /// </summary>
    public static IEnumerable<TokenType> Stream
    {
        get
        {
            yield return new TokenType(
                Tag: CommentTag,
                Pattern: "[/]{2}.*",
                Priority: 0,
                CanIgnore: true);
            
            yield return new TokenType(
                Tag: DoubleLiteralTag,
                Pattern: "[0-9]+[.][0-9]+",
                Priority: 10);
            
            yield return new TokenType(
                Tag: IntLiteralTag,
                Pattern: "[0-9]+",
                Priority: 20);
            
            yield return new TokenType(
                Tag: BooleanLiteralTag,
                Pattern: "(?<![a-zA-Z0-9])(true|false)(?![a-zA-Z0-9])",
                Priority: 30);
            
            yield return new TokenType(
                Tag: NullLiteralTag,
                Pattern: "(?<![a-zA-Z0-9])(null)(?![a-zA-Z0-9])",
                Priority: 40);
            
            yield return new TokenType(
                Tag: StringLiteralTag,
                Pattern: """
                         \"(\\.|[^"\\])*\"
                         """,
                Priority: 50);
            
            yield return new TokenType(
                Tag: KeywordTag,
                Pattern:
                "(?<![a-zA-Z0-9])(namespace|class|if|else|while|for|break|continue|return)(?![a-zA-Z0-9])",
                Priority: 60);
            
            yield return new TokenType(
                Tag: VoidTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(void)(?![a-zA-Z0-9])",
                Priority: 61);
            
            yield return new TokenType(
                Tag: VarTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(var)(?![a-zA-Z0-9])",
                Priority: 62);
            
            yield return new TokenType(
                Tag: IntTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(int)(?![a-zA-Z0-9])",
                Priority: 63);
            
            yield return new TokenType(
                Tag: LongTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(long)(?![a-zA-Z0-9])",
                Priority: 64);
            
            yield return new TokenType(
                Tag: DoubleTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(double)(?![a-zA-Z0-9])",
                Priority: 65);
            
            yield return new TokenType(
                Tag: BooleanTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(bool)(?![a-zA-Z0-9])",
                Priority: 66);
            
            yield return new TokenType(
                Tag: StringTypeTag,
                Pattern: "(?<![a-zA-Z0-9])(string)(?![a-zA-Z0-9])",
                Priority: 67);
            
            yield return new TokenType(
                Tag: AttributeTag,
                Pattern:
                ".*@[A-Za-z0-9]*",
                Priority: 70);
            
            yield return new TokenType(
                Tag: IdentifierTag,
                Pattern: "[a-zA-Z][a-zA-Z0-9]*",
                Priority: 80);
            
            yield return new TokenType(
                Tag: OperatorTag,
                Pattern: "[+]{1,2}|[-]{1,2}|[*]|[/]|[%]|([!]|[=])[=]|([<]|[>])[=]?|[!]|[?]{2}|[|]{2}|[&]{2}|[~]",
                Priority: 90);
            
            yield return new TokenType(
                Tag: QuestionTag,
                Pattern: "[?]",
                Priority: 100);
            
            yield return new TokenType(
                Tag: ColonTag,
                Pattern: "[:]",
                Priority: 110);
            
            yield return new TokenType(
                Tag: SemicolonTag,
                Pattern: "[;]",
                Priority: 120);
            
            yield return new TokenType(
                Tag: AssignTag,
                Pattern: "[=]",
                Priority: 130);
            
            yield return new TokenType(
                Tag: CommaTag,
                Pattern: "[,]",
                Priority: 140);
            
            yield return new TokenType(
                Tag: LeftCurlTag,
                Pattern: "[{]",
                Priority: 150);
            
            yield return new TokenType(
                Tag: RightCurlTag,
                Pattern: "[}]",
                Priority: 160);
            
            yield return new TokenType(
                Tag: LeftParenTag,
                Pattern: "[(]",
                Priority: 170);
            
            yield return new TokenType(
                Tag: RightParenTag,
                Pattern: "[)]",
                Priority: 180);
        }
    }
    
    /// <summary>
    /// Тип токена
    /// </summary>
    public readonly record struct TokenType
    {
        internal TokenType(
            string Tag,
            string Pattern,
            int Priority,
            bool CanIgnore = false)
        {
            this.Tag = Tag;
            this.Pattern = Pattern;
            this.Priority = Priority;
            this.CanIgnore = CanIgnore;
        }
        
        /// <summary>
        /// Тег
        /// </summary>
        public string Tag { get; }
        
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
        
        /// <inheritdoc cref="object.ToString" />
        public override string ToString() => Tag;
    }
}