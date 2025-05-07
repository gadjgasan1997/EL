using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение функции
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class FunctionDeclaration : Declaration
{
    /// <summary>
    /// Возвращаемый тип
    /// </summary>
    public ElTypeNode ReturnType { get; }
    
    /// <summary>
    /// Название
    /// </summary>
    public IdentifierExpression Name { get; }
    
    /// <summary>
    /// Список параметров
    /// </summary>
    public IReadOnlyList<FunctionParameterDeclaration> Parameters { get; }
    
    /// <summary>
    /// Тело
    /// </summary>
    public StatementsBlock Body { get; }
    
    /// <summary>
    /// Выражения возврата результата
    /// </summary>
    public IReadOnlyList<ReturnStatement> ReturnStatements { get; }
    
    public FunctionDeclaration(
        ElTypeNode returnType,
        IdentifierExpression name,
        List<FunctionParameterDeclaration> parameters,
        StatementsBlock body)
    {
        Name = name;
        Name.Parent = this;
        
        ReturnType = returnType;
        Parameters = parameters;
        
        foreach (var parameter in Parameters)
            parameter.Parent = this;
        
        Body = body;
        Body.Parent = this;
        
        ReturnStatements = body.GetAllNodes().OfType<ReturnStatement>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Body];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"function::{Name}";
}