using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;

/// <summary>
/// Выражение присваивания значения переменной
/// </summary>
public class VariableAssignmentExpression : LeftHandSideExpression
{
    public VariableAssignmentExpression(IdentifierExpression id)
    {
        Id = id;
    }
    
    /// <inheritdoc cref="LeftHandSideExpression.Id" />
    public override IdentifierExpression Id { get; }
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"variable::{Id.Name}";
}