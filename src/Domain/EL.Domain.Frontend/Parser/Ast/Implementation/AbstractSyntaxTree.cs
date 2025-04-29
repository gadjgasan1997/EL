using System.Text;
// ReSharper disable ForCanBeConvertedToForeach

namespace EL.Domain.Frontend.Parser.Ast.Implementation;

/// <inheritdoc cref="IAbstractSyntaxTree" />
internal class AbstractSyntaxTree(IAbstractSyntaxTreeNode root) : IAbstractSyntaxTree
{
    /// <inheritdoc cref="IAbstractSyntaxTree.Root" />
    public IAbstractSyntaxTreeNode Root { get; } = root;
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString()
    {
        var builder = new StringBuilder("digraph ast {\n");
        var nodes = Root.GetAllNodes();
        
        for (var i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            builder.Append('\t').Append(node).Append('\n');
            
            for (var j = 0; j < node.Count; j++)
            {
                var child = node[j];
                builder.Append($"\t{node.GetHashCode()}->{child.GetHashCode()}\n");
            }
        }
        
        return builder.Append("}\n").ToString();
    }
}