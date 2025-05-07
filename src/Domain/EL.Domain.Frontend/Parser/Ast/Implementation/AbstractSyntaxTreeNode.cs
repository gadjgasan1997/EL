using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EL.Domain.Frontend.Parser.Extensions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation;

/// <inheritdoc cref="IAbstractSyntaxTreeNode" />
public abstract class AbstractSyntaxTreeNode : IAbstractSyntaxTreeNode
{
    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
    public IEnumerator<IAbstractSyntaxTreeNode> GetEnumerator() => Children.GetEnumerator();
    
    /// <inheritdoc cref="IEnumerable.GetEnumerator" />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    /// <inheritdoc cref="IReadOnlyCollection{T}.Count" />
    public int Count => Children.Count;
    
    /// <inheritdoc cref="IReadOnlyList{T}.this" />
    public IAbstractSyntaxTreeNode this[int index] => Children[index];
    
    /// <inheritdoc cref="IVisitable{T}.Accept{T}" />
    public virtual TReturn Accept<TReturn>(IVisitor<IAbstractSyntaxTreeNode, TReturn> visitor) =>
        visitor.DefaultVisit;
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.Parent" />
    public IAbstractSyntaxTreeNode? Parent { get; set; }
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.GetClosestNode{TNode}" />
    public TNode? GetClosestNode<TNode>()
        where TNode : class, IAbstractSyntaxTreeNode
    {
        var parent = Parent;
        do
        {
            if (parent is TNode node)
                return node;
        } while((parent = parent?.Parent) is not null);
        
        return null;
    }
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.GetAllNodes" />
    public IReadOnlyList<IAbstractSyntaxTreeNode> GetAllNodes() =>
        new TraverseEnumerator(this).ToList();
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.Scope" />
    public Scope? Scope { get; private set; }
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.InitScope" />
    [MemberNotNull(nameof(Scope))]
    public void InitScope()
    {
        if (Scope is not null)
            throw new InvalidOperationException("'Scope' must be null");
        
        Scope = new Scope();
    }
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.SetExistingScope" />
    public void SetExistingScope(Scope scope)
    {
        if (Scope is not null)
            throw new InvalidOperationException("'Scope' must be null");
        
        Scope = scope.ShouldBeInitialized();
    }
    
    /// <summary>
    /// Метод возвращает <c>true</c>, если узел - потомок заданного типа и выполняется заданное условие.<br/>
    /// В случае, когда условие не задано, проверяется просто соответствие типов.
    /// </summary>
    /// <param name="condition">Условие для родителя</param>
    /// <typeparam name="T">Проверяемый тип родителя</typeparam>
    public bool IsChildOf<T>(Predicate<T>? condition = null)
        where T : IAbstractSyntaxTreeNode
    {
        IAbstractSyntaxTreeNode? parent = this;
        while ((parent = parent.Parent) is not null)
        {
            if (parent is T node)
                return condition?.Invoke(node) ?? true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Метод возвращает <c>true</c>, если узел - потомок заданного типа.<br/>
    /// </summary>
    /// <typeparam name="T1">Проверяемый тип родителя</typeparam>
    /// <typeparam name="T2">Проверяемый тип родителя</typeparam>
    public bool IsChildOf<T1, T2>()
        where T1 : IAbstractSyntaxTreeNode
        where T2 : IAbstractSyntaxTreeNode
    {
        IAbstractSyntaxTreeNode? parent = this;
        while ((parent = parent.Parent) is not null)
        {
            if (parent is T1 or T2)
                return true;
        }
        
        return false;
    }
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => $"{GetHashCode()} [label=\"{NodeRepresentation()}\"]";
    
    /// <summary>
    /// Отображение ноды
    /// </summary>
    /// <returns></returns>
    protected abstract string NodeRepresentation();
    
    /// <summary>
    /// Дочерние ноды
    /// </summary>
    protected virtual IReadOnlyList<IAbstractSyntaxTreeNode> Children { get; } = [];
}