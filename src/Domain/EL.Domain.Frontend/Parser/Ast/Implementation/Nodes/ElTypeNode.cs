using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

/// <summary>
/// Тип языка El
/// </summary>
/// <param name="Id">Id</param>
public record ElTypeNode(IdentifierExpression Id)
{
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => Id;
}