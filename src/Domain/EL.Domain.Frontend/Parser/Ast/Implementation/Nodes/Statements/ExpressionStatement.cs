using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Определение выражения
/// </summary>
public class ExpressionStatement : Statement
{
    /// <summary>
    /// Выражение
    /// </summary>
    public Expression Expression { get; }
    
    public ExpressionStatement(Expression expression)
    {
        Expression = expression;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Expression];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Expression.ToString();
}