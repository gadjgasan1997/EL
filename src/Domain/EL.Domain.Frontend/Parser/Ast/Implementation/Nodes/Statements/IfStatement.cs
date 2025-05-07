using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Выражение условия if
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class IfStatement : Statement
{
    /// <summary>
    /// Условие
    /// </summary>
    public Expression Condition { get; }
    
    /// <summary>
    /// Действие, выполняемое в случае, если условие равно true
    /// </summary>
    public StatementsBlock Then { get; }
    
    /// <summary>
    /// Действие, выполняемое в случае, если условие равно false
    /// </summary>
    public StatementsBlock? Else { get; }
    
    public IfStatement(
        Expression condition,
        StatementsBlock then,
        StatementsBlock? @else = null)
    {
        Condition = condition;
        Condition.Parent = this;
        
        Then = then;
        Then.Parent = this;
        
        Else = @else;
        if (Else is not null)
            Else.Parent = this;
    }
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children =>
        Else is null ? [Condition, Then] : [Condition, Then, Else];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "if";
}