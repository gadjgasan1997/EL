using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Не все ветви кода функции возвращают значение
/// </summary>
public class NotAllCodeBranchesReturnsValue : SemanticException
{
    internal NotAllCodeBranchesReturnsValue(FunctionDeclaration functionDeclaration)
        : base($"Не все ветви кода функции '{functionDeclaration.Name}' возвращают значение")
    { }
}