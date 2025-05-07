using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;

/// <summary>
/// Определение вызова функции
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public sealed partial class CallExpression : LeftHandSideExpression
{
    /// <summary>
    /// Параметры
    /// </summary>
    public IReadOnlyCollection<CallParameterExpression> Parameters { get; }
    
    public CallExpression(
        IdentifierExpression id,
        IReadOnlyCollection<CallParameterExpression> parameters)
    {
        Id = id;
        Id.Parent = this;
        
        var list = new List<CallParameterExpression>(parameters);
        list.ForEach(parameter => parameter.Parent = this);
        Parameters = list;
    }
    
    /// <inheritdoc cref="LeftHandSideExpression.Id" />
    public override IdentifierExpression Id { get; }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => Parameters.ToList();
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"call::{Id}";
}