using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Функция не должна возвращать значение, если ее результирующий тип - void
/// </summary>
public class FunctionShouldNotReturnValue : SemanticException
{
    internal FunctionShouldNotReturnValue(
        FunctionDeclaration functionDeclaration,
        ElType returnStatementType)
        : base(
            $"Функция '{functionDeclaration.Name}' не должна возвращать значение, " +
            $"так как ее результирующий тип '{ElType.VoidType}'. " +
            $"Происходит попытка вернуть из функции значение с типом '{returnStatementType}'")
    { }
}