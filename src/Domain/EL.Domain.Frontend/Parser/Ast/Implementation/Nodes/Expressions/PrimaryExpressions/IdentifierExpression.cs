namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

/// <summary>
/// Выражение идентификатора
/// </summary>
/// <param name="name">Название</param>
public class IdentifierExpression(string name) : PrimaryExpression
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; } = name;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => Name;
    
    public static implicit operator string(IdentifierExpression identifierExpression) =>
        identifierExpression.Name;
}