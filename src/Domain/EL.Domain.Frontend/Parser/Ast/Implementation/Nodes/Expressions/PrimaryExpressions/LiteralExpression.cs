namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

/// <summary>
/// Выражение символа
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class LiteralExpression : PrimaryExpression
{
    /// <summary>
    /// Тип
    /// </summary>
    public ElTypeNode Type { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public object? Value { get; }
    
    public LiteralExpression(ElTypeNode type, object? value)
    {
        Type = type;
        Value = value;
    }
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"literal::{Value}";
}