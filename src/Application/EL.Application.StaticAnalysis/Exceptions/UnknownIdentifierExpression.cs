using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Использование необъявленной переменной
/// </summary>
public class UnknownIdentifierExpression : SemanticException
{
    internal UnknownIdentifierExpression(IdentifierExpression expression)
        : base($"Использование необъявленной переменной: '{expression.Name}'")
    { }
}