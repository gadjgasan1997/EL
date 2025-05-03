using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Выражение возврата результата
/// </summary>
public class ReturnStatement : Statement
{
    /// <summary>
    /// Выражение возвращаемого результата
    /// </summary>
    public Expression? Expression { get; }
    
    public ReturnStatement(Expression? expression = null)
    {
        Expression = expression;
        if (Expression is not null)
            Expression.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children =>
        Expression is null ? [] : [Expression];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "return";
}