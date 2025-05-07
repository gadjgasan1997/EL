using System.Diagnostics.CodeAnalysis;

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
    IAbstractSyntaxTreeNode? Parent { get; }
    
    /// <summary>
    /// Возвращает ближайщую ноду по типу
    /// </summary>
    /// <typeparam name="TNode">Тип ноды</typeparam>
    /// <returns>Нода</returns>
    TNode? GetClosestNode<TNode>() where TNode : class, IAbstractSyntaxTreeNode;
    
    /// <summary>
    /// Возвращает список всех нод
    /// </summary>
    IReadOnlyList<IAbstractSyntaxTreeNode> GetAllNodes();
    
    /// <summary>
    /// Область видимости
    /// </summary>
    Scope? Scope { get; }
    
    /// <summary>
    /// Инициализирует область видимости
    /// </summary>
    [MemberNotNull(nameof(Scope))]
    void InitScope();
    
    /// <summary>
    /// Проставляет существующую область видимости
    /// </summary>
    /// <param name="scope">Область видимости</param>
    [MemberNotNull(nameof(Scope))]
    void SetExistingScope(Scope scope);
}