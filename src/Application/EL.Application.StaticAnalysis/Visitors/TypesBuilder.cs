using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using EL.Application.StaticAnalysis.Exceptions;
using static EL.Domain.Share.Dictionaries.ElType;

namespace EL.Application.StaticAnalysis.Visitors;

/// <summary>
/// Билдер типов
/// </summary>
internal class TypesBuilder : VisitorBase<IAbstractSyntaxTreeNode, ElType>,
    IVisitor<VariableDeclaration, ElType>,
    IVisitor<FunctionParameterDeclaration, ElType>,
    IVisitor<LiteralExpression, ElType>
{
    public ElType Visit(VariableDeclaration visitable)
    {
        if (visitable.InitializeExpression is null)
            return CreateTypeFromNode(visitable.Type);
        
        var destinationType = CreateTypeFromNode(visitable.Type);
        var sourceType = visitable.InitializeExpression.Source.Accept(This);
        if (destinationType == VarType)
            return sourceType;
        
        if (destinationType != sourceType)
            throw new AssigmentIncomparableTypes(destinationType, sourceType);
        
        return destinationType;
    }
    
    public ElType Visit(FunctionParameterDeclaration visitable) => CreateTypeFromNode(visitable.Type);
    
    public ElType Visit(LiteralExpression visitable) => CreateTypeFromNode(visitable.Type);
    
    private static ElType CreateTypeFromNode(ElTypeNode node) => node.Id.Name;
}