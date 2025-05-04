using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение параметра
/// </summary>
public class FunctionParameterDeclaration : Declaration
{
    /// <summary>
    /// Тип
    /// </summary>
    public ElTypeNode Type { get; }
    
    /// <summary>
    /// Название
    /// </summary>
    public IdentifierExpression Name { get; }
    
    public FunctionParameterDeclaration(
        ElTypeNode type,
        IdentifierExpression name)
    {
        Type = type;
        Name = name;
    }
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override string NodeRepresentation() => $"parameter::{Name.Name}";
}