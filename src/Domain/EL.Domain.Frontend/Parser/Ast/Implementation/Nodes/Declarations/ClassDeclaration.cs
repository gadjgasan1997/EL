using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение класса
/// </summary>
public class ClassDeclaration : Declaration
{
    /// <summary>
    /// Название
    /// </summary>
    private IdentifierExpression Name { get; }
    
    /// <summary>
    /// Тело
    /// </summary>
    public StatementsBlock Body { get; }
    
    /// <summary>
    /// Функции
    /// </summary>
    public IReadOnlyCollection<FunctionDeclaration> Functions { get; }
    
    public ClassDeclaration(
        IdentifierExpression name,
        StatementsBlock body)
    {
        Name = name;
        
        Body = body;
        Body.Parent = this;
        
        Functions = body.GetAllNodes().OfType<FunctionDeclaration>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Body];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"class::{Name.Name}";
}