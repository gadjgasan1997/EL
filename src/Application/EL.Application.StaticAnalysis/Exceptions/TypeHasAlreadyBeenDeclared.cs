using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Тип уже был объявлен в данном пространстве имен
/// </summary>
public class TypeHasAlreadyBeenDeclared : SemanticException
{
    internal TypeHasAlreadyBeenDeclared(
        NamespaceDeclaration namespaceDeclaration,
        ClassDeclaration classDeclaration)
        : base(
            $"В пространстве имен '{namespaceDeclaration.Name}' " +
            $"уже был объявлен тип '{classDeclaration.Name}'")
    { }
}