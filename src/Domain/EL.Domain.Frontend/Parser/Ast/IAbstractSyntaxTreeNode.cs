using Visitor.NET;

namespace EL.Domain.Frontend.Parser.Ast;

/// <summary>
/// Нода дерева
/// </summary>
public interface IAbstractSyntaxTreeNode :
    IReadOnlyList<IAbstractSyntaxTreeNode>,
    IVisitable<IAbstractSyntaxTreeNode>
{
    /// <summary>
    /// Родительская нода
    /// </summary>
    public IAbstractSyntaxTreeNode Parent { get; }
    
    /// <summary>
    /// Возвращает список всех нод
    /// </summary>
    public IReadOnlyList<IAbstractSyntaxTreeNode> GetAllNodes();
}