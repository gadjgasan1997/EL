using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;

/// <summary>
/// Выражение левой части присваивания
/// </summary>
public abstract class LeftHandSideExpression : Expression
{
    /// <summary>
    /// Id
    /// </summary>
    public abstract IdentifierExpression Id { get; }
}