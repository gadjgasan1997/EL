using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Переменная уже была объявлена внутри области видимости
/// </summary>
public class SameVariableHasAlreadyBeenDeclared : SemanticException
{
    internal SameVariableHasAlreadyBeenDeclared(VariableDeclaration variable)
        : base(
            $"Нельзя объявить переменную с типом '{variable.Type}' и названием '{variable.Name}' " +
            "так как такая переменная уже была объявлена в данной области видимости.")
    { }
}