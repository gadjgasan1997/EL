using EL.Domain.Share.SeedWork;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Оператор
/// </summary>
public class Operator : Enumeration
{
    public const string PLUS = "+";
    public static Operator Plus { get; } = new(PLUS);
    
    public const string MINUS = "-";
    public static Operator Minus { get; } = new(MINUS);
    
    public const string MULTIPLY = "*";
    public static Operator Multiply { get; } = new(MULTIPLY);
    
    public const string DIVIDE = "/";
    public static Operator Divide { get; } = new(DIVIDE);
    
    public const string REMAINDER = "%";
    public static Operator Remainder { get; } = new(REMAINDER);
    
    public const string EQUAL = "==";
    public static Operator Equal { get; } = new(EQUAL);
    
    public const string NOT_EQUAL = "!=";
    public static Operator NotEqual { get; } = new(NOT_EQUAL);
    
    public const string LESS_THAN = "<";
    public static Operator LessThan { get; } = new(LESS_THAN);
    
    public const string GREATER_THAN = ">";
    public static Operator GreaterThan { get; } = new(GREATER_THAN);
    
    public const string LESS_THAN_OR_EQUAL = "<=";
    public static Operator LessThanOrEqual { get; } = new(LESS_THAN_OR_EQUAL);
    
    public const string GREATER_THAN_OR_EQUAL = ">=";
    public static Operator GreaterThanOrEqual { get; } = new(GREATER_THAN_OR_EQUAL);
    
    public const string NOT = "!";
    public static Operator Not { get; } = new(NOT);
    
    public const string AND = "&&";
    public static Operator And { get; } = new(AND);
    
    public const string OR = "||";
    public static Operator Or { get; } = new(OR);
    
    public const string NULL_COALESCING = "??";
    public static Operator NullCoalescing { get; } = new(NULL_COALESCING);
    
    public const string INCREMENT = "++";
    public static Operator Increment { get; } = new(INCREMENT);
    
    public const string DECREMENT = "--";
    public static Operator Decrement { get; } = new(DECREMENT);
    
    /// <summary>
    /// Создает оператор из строки
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Оператор</returns>
    public static implicit operator Operator(string value)
    {
        return value switch
        {
            PLUS => Plus,
            MINUS => Minus,
            MULTIPLY => Multiply,
            DIVIDE => Divide,
            REMAINDER => Remainder,
            EQUAL => Equal,
            NOT_EQUAL => NotEqual,
            LESS_THAN => LessThan,
            GREATER_THAN => GreaterThan,
            LESS_THAN_OR_EQUAL => LessThanOrEqual,
            GREATER_THAN_OR_EQUAL => GreaterThanOrEqual,
            NOT => Not,
            AND => And,
            OR => Or,
            NULL_COALESCING => NullCoalescing,
            INCREMENT => Increment,
            DECREMENT => Decrement,
            
            _ => throw new InvalidOperationException($"Строка '{value}' не является оператором")
        };
    }
    
    private Operator(string value) : base(value)
    { }
}