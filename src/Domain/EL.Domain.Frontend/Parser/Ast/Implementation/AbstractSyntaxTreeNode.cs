using Visitor.NET;
using System.Collections;

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
    public TReturn Accept<TReturn>(IVisitor<IAbstractSyntaxTreeNode, TReturn> visitor) =>
        visitor.DefaultVisit;
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.Parent" />
    public IAbstractSyntaxTreeNode Parent { get; set; } = default!;
    
    /// <inheritdoc cref="IAbstractSyntaxTreeNode.GetAllNodes" />
    public IReadOnlyList<IAbstractSyntaxTreeNode> GetAllNodes() =>
        new TraverseEnumerator(this).ToList();
    
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