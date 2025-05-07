using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Возвращаемый из функции тип не соответствует результирующему типу функции
/// </summary>
public class UnacceptableFunctionReturnType : SemanticException
{
    internal UnacceptableFunctionReturnType(
        FunctionDeclaration functionDeclaration,
        ElType expectedReturnType,
        ElType actualReturnType)
        : base(
            $"Происходит попытка вернуть из функции '{functionDeclaration.Name}' значение, " +
            "не соответствующее ее результирующему типу. " +
            $"Функция имеет результирующий тип '{expectedReturnType}'. " +
            $"При этом возвращается тип '{actualReturnType}'")
    { }
}