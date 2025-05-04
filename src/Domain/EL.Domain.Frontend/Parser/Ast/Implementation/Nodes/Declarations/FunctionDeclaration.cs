using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;

/// <summary>
/// Определение функции
/// </summary>
public class FunctionDeclaration : Declaration
{
    /// <summary>
    /// Возвращаемый тип
    /// </summary>
    public ElTypeNode ReturnType { get; }
    
    /// <summary>
    /// Название
    /// </summary>
    private IdentifierExpression Name { get; }
    
    /// <summary>
    /// Список параметров
    /// </summary>
    public IReadOnlyCollection<FunctionParameterDeclaration> Parameters { get; }
    
    /// <summary>
    /// Тело
    /// </summary>
    public StatementsBlock Body { get; }
    
    /// <summary>
    /// Выражения возврата результата
    /// </summary>
    public IReadOnlyCollection<ReturnStatement> ReturnStatements { get; }
    
    public FunctionDeclaration(
        ElTypeNode returnType,
        IdentifierExpression name,
        List<FunctionParameterDeclaration> parameters,
        StatementsBlock body)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
        
        Body = body;
        Body.Parent = this;
        
        ReturnStatements = body.GetAllNodes().OfType<ReturnStatement>().ToList();
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => [Body];
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => $"function::{Name.Name}";
}