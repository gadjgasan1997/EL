namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;

/// <summary>
/// Выражение присванивания
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class AssignmentExpression : Expression
{
    /// <summary>
    /// Конструкция, в которую происходит присваивание
    /// </summary>
    public Expression Destination { get; }
    
    /// <summary>
    /// Конструкция, присваиваемая объекту <see cref="Destination" />
    /// </summary>
    public Expression Source { get; }
    
    public AssignmentExpression(
        Expression destination,
        Expression source)
    {
        Destination = destination;
        Destination.Parent = this;
        
        Source = source;
        Source.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Destination, Source];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "=";
}