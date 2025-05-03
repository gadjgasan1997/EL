using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Выражение цикла for
/// </summary>
public class ForStatement : Statement
{
    /// <summary>
    /// Объявление переменной
    /// </summary>
    public VariableDeclaration? Declaration { get; }
    
    /// <summary>
    /// Условие
    /// </summary>
    public Expression? Condition { get; }
    
    /// <summary>
    /// Итерация
    /// </summary>
    public Expression? Iteration { get; }
    
    /// <summary>
    /// Тело
    /// </summary>
    public Statement Body { get; }
    
    public ForStatement(
        VariableDeclaration? declaration,
        Expression? condition,
        Expression? iteration,
        Statement body)
    {
        Declaration = declaration;
        if (Declaration is not null)
            Declaration.Parent = this;
        
        Condition = condition;
        if (Condition is not null)
            Condition.Parent = this;
        
        Iteration = iteration;
        if (Iteration is not null)
            Iteration.Parent = this;
        
        Body = body;
        Body.Parent = this;
    }
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => false;
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => GetChildren().ToList();
    
    private IEnumerable<IAbstractSyntaxTreeNode> GetChildren()
    {
        if (Declaration is not null)
            yield return Declaration;
        
        if (Condition is not null)
            yield return Condition;
        
        if (Iteration is not null)
            yield return Iteration;
        
        yield return Body;
    }
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "for";
}