using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
// ReSharper disable ForCanBeConvertedToForeach

namespace EL.Application.StaticAnalysis.Visitors;

/// <summary>
/// Визитор валидации наличия выражений return из инструкций
/// </summary>
internal class StatementsReturnsValidator : VisitorBase<IAbstractSyntaxTreeNode, bool>,
    IVisitor<FunctionDeclaration, bool>,
    IVisitor<IfStatement, bool>,
    IVisitor<StatementsBlock, bool>
{
    public bool Visit(FunctionDeclaration visitable) => visitable.Body.Accept(This);
    
    public bool Visit(StatementsBlock visitable)
    {
        return visitable.Statements.Any(item => item is ReturnStatement) ||
               visitable.Any(node => node.Accept(This));
    }
    
    public bool Visit(IfStatement statement) =>
        statement.Then.Accept(This) &&
        (statement.Else is null || statement.Else.Accept(This));
}