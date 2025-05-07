using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Переменная объявлена за пределами функции
/// </summary>
public class VariableOutsideOfFunction : SemanticException
{
    internal VariableOutsideOfFunction(VariableDeclaration declaration)
        : base($"Переменная может быть объявлена только внутри функции. Переменная: '{declaration.Name}'")
    { }
}