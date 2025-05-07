using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение переменной
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class VariableDeclaration : Declaration
{
    /// <summary>
    /// Тип
    /// </summary>
    public ElTypeNode Type { get; }
    
    /// <summary>
    /// Название
    /// </summary>
    public IdentifierExpression Name { get; }
    
    /// <summary>
    /// Выражение инициализации
    /// </summary>
    public AssignmentExpression? InitializeExpression { get; }
    
    public VariableDeclaration(
        ElTypeNode type,
        IdentifierExpression name,
        AssignmentExpression? initializeExpression = null)
    {
        Type = type;
        Type.Id.Parent = this;
        
        Name = name;
        Name.Parent = this;
        
        InitializeExpression = initializeExpression;
        if (InitializeExpression is not null)
            InitializeExpression.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children =>
        InitializeExpression is null
            ? [Type.Id, Name]
            : [Type.Id, Name, InitializeExpression];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"variable::{Name}";
}