using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Выражение break объявлено за пределами цикла
/// </summary>
public class BreakOutsideOfCycle : SemanticException
{
    internal BreakOutsideOfCycle(BreakStatement statement)
        : base("Выражение break может быть объявлено только внутри цикла")
    { }
}