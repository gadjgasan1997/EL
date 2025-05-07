using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение пространства имен
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class NamespaceDeclaration : Declaration
{
    /// <summary>
    /// Название
    /// </summary>
    public IdentifierExpression Name { get; }
    
    /// <summary>
    /// Тело
    /// </summary>
    public StatementsBlock Body { get; }
    
    /// <summary>
    /// Классы
    /// </summary>
    public IReadOnlyList<ClassDeclaration> ChildrenClasses { get; }
    
    public NamespaceDeclaration(IdentifierExpression name, StatementsBlock body)
    {
        Name = name;
        Name.Parent = this;
        
        Body = body;
        Body.Parent = this;
        
        ChildrenClasses = body.Statements.OfType<ClassDeclaration>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => ChildrenClasses;
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"namespace::{Name}";
}