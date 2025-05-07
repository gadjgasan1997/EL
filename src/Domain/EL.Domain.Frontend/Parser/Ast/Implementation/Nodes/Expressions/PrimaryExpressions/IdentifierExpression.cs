namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

/// <summary>
/// Выражение идентификатора
/// </summary>
/// <param name="name">Название</param>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class IdentifierExpression(string name) : PrimaryExpression, IEquatable<IdentifierExpression>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; } = name;
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => NodeRepresentation();
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Name;
    
    /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
    public virtual bool Equals(IdentifierExpression? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.GetType() != GetType()) return false;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
    }
    
    /// <inheritdoc cref="IEquatable{T}.Equals(object)" />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((IdentifierExpression) obj);
    }
    
    /// <inheritdoc cref="IEquatable{T}.GetHashCode" />
    public override int GetHashCode() => Name.GetHashCode();
    
    public static bool operator ==(IdentifierExpression left, IdentifierExpression right) =>
        Equals(left, right);
    
    public static bool operator !=(IdentifierExpression left, IdentifierExpression right) =>
        !(left == right);
    
    public static implicit operator string(IdentifierExpression identifierExpression) =>
        identifierExpression.Name;
}