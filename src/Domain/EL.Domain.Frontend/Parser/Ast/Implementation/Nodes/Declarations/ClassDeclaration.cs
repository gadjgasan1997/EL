using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение класса
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class ClassDeclaration : Declaration
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
    /// Функции
    /// </summary>
    public IReadOnlyList<FunctionDeclaration> Functions { get; }
    
    public ClassDeclaration(
        IdentifierExpression name,
        StatementsBlock body)
    {
        Name = name;
        Name.Parent = this;
        
        Body = body;
        Body.Parent = this;
        
        Functions = body.GetAllNodes().OfType<FunctionDeclaration>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Body];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"class::{Name}";
}