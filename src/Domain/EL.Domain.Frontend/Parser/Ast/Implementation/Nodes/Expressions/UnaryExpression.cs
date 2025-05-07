using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

/// <summary>
/// Унарное выражение
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class UnaryExpression : Expression
{
    /// <summary>
    /// Оператор
    /// </summary>
    public Operator Operator { get; }
    
    /// <summary>
    /// Выражение
    /// </summary>
    public Expression Expression { get; }
    
    public UnaryExpression(Operator @operator, Expression expression)
    {
        Operator = @operator;
        Expression = expression;
        Expression.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Expression];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Operator.Value;
}