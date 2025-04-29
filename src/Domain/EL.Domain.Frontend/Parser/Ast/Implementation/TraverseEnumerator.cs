using System.Collections;
using System.Runtime.CompilerServices;

namespace EL.Domain.Frontend.Parser.Ast.Implementation;

internal struct TraverseEnumerator :
    IEnumerator<IAbstractSyntaxTreeNode>,
    IEnumerable<IAbstractSyntaxTreeNode>
{
    [ThreadStatic]
    private static Queue<IAbstractSyntaxTreeNode>? _buffer;
    private readonly Queue<IAbstractSyntaxTreeNode> _queue;
    private IAbstractSyntaxTreeNode _current;
    
    public TraverseEnumerator(IAbstractSyntaxTreeNode parent)
    {
        var queue = _buffer ?? new Queue<IAbstractSyntaxTreeNode>(128);
        _buffer = null;
        
        queue.Enqueue(parent);
        
        _queue = queue;
        _current = null!;
    }
    
    /// <inheritdoc cref="IEnumerator{T}.Current" />
    public IAbstractSyntaxTreeNode Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _current;
    }
    
    /// <inheritdoc cref="IEnumerator.Current" />
    object IEnumerator.Current => Current;
    
    /// <inheritdoc cref="IEnumerator.MoveNext" />
    public bool MoveNext()
    {
        var queue = _queue;
        if (queue.Count == 0)
            return false;
        
        var current = _queue.Dequeue();
        for (var i = 0; i < current.Count; i++)
            queue.Enqueue(current[i]);
        
        _current = current;
        return true;
    }
    
    /// <inheritdoc cref="IEnumerator.Reset" />
    public void Reset()
    { }
    
    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<IAbstractSyntaxTreeNode> GetEnumerator() => this;
    
    /// <inheritdoc cref="IEnumerable.GetEnumerator" />
    IEnumerator IEnumerable.GetEnumerator() => this;
    
    /// <inheritdoc cref="IDisposable.Dispose" />
    public void Dispose()
    {
        _queue.Clear();
        _buffer = _queue;
    }
}