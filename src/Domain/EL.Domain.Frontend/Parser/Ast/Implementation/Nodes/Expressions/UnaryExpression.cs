namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

/// <summary>
/// Унарное выражение
/// </summary>
public class UnaryExpression : Expression
{
    /// <summary>
    /// Оператор
    /// </summary>
    public string Operator { get; }
    
    /// <summary>
    /// Выражение
    /// </summary>
    public Expression Expression { get; }
    
    public UnaryExpression(string @operator, Expression expression)
    {
        Operator = @operator;
        Expression = expression;
        Expression.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Expression];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Operator;
}