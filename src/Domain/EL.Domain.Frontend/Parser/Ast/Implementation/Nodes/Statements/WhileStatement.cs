using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Выражение цикла while
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class WhileStatement : Statement
{
    /// <summary>
    /// Условие
    /// </summary>
    public Expression Condition { get; }
    
    /// <summary>
    /// Действие, выполняемое в случае, если условие равно true
    /// </summary>
    public StatementsBlock Then { get; }
    
    public WhileStatement(Expression condition, StatementsBlock then)
    {
        Condition = condition;
        Condition.Parent = this;
        
        Then = then;
        Then.Parent = this;
    }
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Condition, Then];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "while";
}