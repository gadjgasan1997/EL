namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

/// <summary>
/// Бинарное выражение
/// </summary>
public class BinaryExpression : Expression
{
    /// <summary>
    /// Левое выражение
    /// </summary>
    public Expression Left { get; }
    
    /// <summary>
    /// Оператор
    /// </summary>
    public string Operator { get; }
    
    /// <summary>
    /// Правое выражение
    /// </summary>
    public Expression Right { get; }
    
    public BinaryExpression(Expression left, string @operator, Expression right)
    {
        Left = left;
        Left.Parent = this;
        
        Operator = @operator;
        
        Right = right;
        Right.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Left, Right];
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Operator;
}