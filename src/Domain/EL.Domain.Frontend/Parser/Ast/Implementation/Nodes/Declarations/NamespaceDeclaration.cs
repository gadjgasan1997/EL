using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение пространства имен
/// </summary>
public class NamespaceDeclaration : Declaration
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
    /// Классы
    /// </summary>
    public IReadOnlyCollection<ClassDeclaration> Classes { get; }
    
    public NamespaceDeclaration(
        IdentifierExpression name,
        StatementsBlock body)
    {
        Name = name;
        
        Body = body;
        Body.Parent = this;
        
        Classes = body.GetAllNodes().OfType<ClassDeclaration>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Body];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"namespace::{Name.Name}";
}