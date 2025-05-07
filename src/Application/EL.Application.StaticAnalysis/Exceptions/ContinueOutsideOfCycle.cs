using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Выражение continue объявлено за пределами цикла
/// </summary>
public class ContinueOutsideOfCycle : SemanticException
{
    internal ContinueOutsideOfCycle(ContinueStatement statement)
        : base("Выражение continue может быть объявлено только внутри цикла")
    { }
}