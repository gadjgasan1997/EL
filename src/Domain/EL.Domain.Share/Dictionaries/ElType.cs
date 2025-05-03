using EL.Domain.Share.SeedWork;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Тип языка El
/// </summary>
public class ElType : Enumeration
{
    public static ElType VarType { get; } = new("var");
    public static ElType DoubleType { get; } = new("double");
    public static ElType IntType { get; } = new("int");
    public static ElType LongType { get; } = new("long");
    public static ElType BooleanType { get; } = new("bool");
    public static ElType NullType { get; } = new("null");
    public static ElType StringType { get; } = new("string");
    
    private ElType(string value) : base(value)
    { }
}