namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

/// <summary>
/// Выражение тернарного оператора
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class TernaryOperatorExpression : Expression
{
    /// <summary>
    /// Условие
    /// </summary>
    public Expression Condition { get; }
    
    /// <summary>
    /// Действие, выполняемое в случае, если условие равно true
    /// </summary>
    public Expression Then { get; }
    
    /// <summary>
    /// Действие, выполняемое в случае, если условие равно false
    /// </summary>
    public Expression Else { get; }
    
    public TernaryOperatorExpression(Expression condition, Expression then, Expression @else)
    {
        Condition = condition;
        Condition.Parent = this;
        
        Then = then;
        Then.Parent = this;
        
        Else = @else;
        Else.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Condition, Then, Else];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "?:";
}