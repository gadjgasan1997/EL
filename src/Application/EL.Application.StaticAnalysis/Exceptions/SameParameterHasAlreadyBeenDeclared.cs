using EL.Domain.IR.Symbols;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Параметр функции с таким названием уже был объявлен
/// </summary>
public class SameParameterHasAlreadyBeenDeclared : SemanticException
{
    internal SameParameterHasAlreadyBeenDeclared(
        ClassDeclaration classDeclaration,
        FunctionDeclaration functionDeclaration,
        Symbol parameterSymbol)
        : base(
            $"В функции '{functionDeclaration.Name}' класса '{classDeclaration.Name}' уже объявлен параметр " +
            $"с названием '{parameterSymbol.Name}'. " +
            $"Названия параметров функций должны быть уникальными")
    { }
}