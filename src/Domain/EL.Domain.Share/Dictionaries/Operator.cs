using EL.Domain.Share.SeedWork;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Оператор
/// </summary>
public class Operator : Enumeration
{
    public static Operator Plus { get; } = new("+");
    public static Operator Minus { get; } = new("-");
    public static Operator Multiply { get; } = new("*");
    public static Operator Divide { get; } = new("/");
    public static Operator Remainder { get; } = new("%");
    public static Operator LessThan { get; } = new("<");
    public static Operator GreaterThan { get; } = new(">");
    public static Operator Equal { get; } = new("==");
    public static Operator NotEqual { get; } = new("!=");
    public static Operator LessThanOrEqual { get; } = new("<=");
    public static Operator GreaterThanOrEqual { get; } = new(">=");
    public static Operator Not { get; } = new("!");
    public static Operator And { get; } = new("&&");
    public static Operator Or { get; } = new("||");
    public static Operator NullCoalescing { get; } = new("??");
    public static Operator Increment { get; } = new("++");
    public static Operator Decrement { get; } = new("--");
    
    private Operator(string value) : base(value)
    { }
}