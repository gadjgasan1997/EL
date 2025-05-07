using EL.Domain.Share.SeedWork;
// ReSharper disable ConvertIfStatementToReturnStatement

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Тип языка El
/// </summary>
public class ElType : Enumeration, IEquatable<ElType>
{
    public const string VOID_TYPE = "void";
    public static ElType VoidType { get; } = new(VOID_TYPE);
    
    public const string VAR_TYPE = "var";
    public static ElType VarType { get; } = new(VAR_TYPE);
    
    public const string DOUBLE_TYPE = "double";
    public static ElType DoubleType { get; } = new(DOUBLE_TYPE);
    
    public const string INT_TYPE = "int";
    public static ElType IntType { get; } = new(INT_TYPE);
    
    public const string LONG_TYPE = "long";
    public static ElType LongType { get; } = new(LONG_TYPE);
    
    public const string BOOLEAN_TYPE = "bool";
    public static ElType BooleanType { get; } = new(BOOLEAN_TYPE);
    
    public const string NULL_TYPE = "null";
    public static ElType NullType { get; } = new(NULL_TYPE);
    
    public const string STRING_TYPE = "string";
    public static ElType StringType { get; } = new(STRING_TYPE);
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => Value;
    
    /// <inheritdoc cref="IEquatable{ElType}.Equals(ElType)" />
    public bool Equals(ElType? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);
    }
    
    /// <inheritdoc cref="object.Equals(object)" />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((ElType) obj);
    }
    
    /// <inheritdoc cref="object.GetHashCode" />
    public override int GetHashCode() => StringComparer.InvariantCultureIgnoreCase.GetHashCode(Value);
    
    public static bool operator ==(ElType? left, ElType? right) => Equals(left, right);
    
    public static bool operator !=(ElType? left, ElType? right) => !Equals(left, right);
    
    public static implicit operator string(ElType type) => type.ToString();
    
    public static implicit operator ElType(string value)
    {
        return value switch
        {
            VOID_TYPE => VoidType,
            VAR_TYPE => VarType,
            DOUBLE_TYPE => DoubleType,
            INT_TYPE => IntType,
            LONG_TYPE => LongType,
            BOOLEAN_TYPE => BooleanType,
            NULL_TYPE => NullType,
            STRING_TYPE => StringType,
            
            _ => VarType
        };
    }
    
    private ElType(string value) : base(value)
    { }
}