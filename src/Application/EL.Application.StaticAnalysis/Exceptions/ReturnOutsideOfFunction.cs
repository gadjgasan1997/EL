using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Выражение return объявлено за пределами тела функции
/// </summary>
public class ReturnOutsideOfFunction : SemanticException
{
    internal ReturnOutsideOfFunction(ReturnStatement statement)
        : base("Выражение return может быть объявлено только внутри тела функции")
    { }
}