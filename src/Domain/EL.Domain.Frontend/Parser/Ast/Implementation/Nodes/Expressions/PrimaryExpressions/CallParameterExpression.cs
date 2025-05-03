namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

/// <summary>
/// Параметр вызова функции
/// </summary>
public class CallParameterExpression : PrimaryExpression
{
    /// <summary>
    /// Значение
    /// </summary>
    public Expression Value { get; }
    
    public CallParameterExpression(Expression value)
    {
        Value = value;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Value];
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override string NodeRepresentation() => "call_parameter";
}