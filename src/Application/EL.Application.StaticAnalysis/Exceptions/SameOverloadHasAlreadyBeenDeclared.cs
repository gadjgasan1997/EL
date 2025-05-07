using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Функция с такой перегрузкой уже была объявлена
/// </summary>
public class SameOverloadHasAlreadyBeenDeclared : SemanticException
{
    private SameOverloadHasAlreadyBeenDeclared(string message)
        : base(message)
    { }
    
    public static SameOverloadHasAlreadyBeenDeclared Create(
        ClassDeclaration classDeclaration,
        FunctionDeclaration functionDeclaration)
    {
        string message;
        if (functionDeclaration.Parameters.Count == 0)
        {
            message = $"В типе '{classDeclaration.Name}' уже объявлена функция '{functionDeclaration.Name}'";
        }
        else
        {
            var parameters = string.Join(
                ", ",
                functionDeclaration.Parameters.Select(parameter => parameter.Type));
            
            message = $"В типе '{classDeclaration.Name}' уже объявлена функция '{functionDeclaration.Name}' " +
                      $"с таким набором параметров: '{parameters}'";
        }
        
        return new SameOverloadHasAlreadyBeenDeclared(message);
    }
}