namespace EL.Domain.Frontend.Parser.Ast;

/// <summary>
/// Дерево
/// </summary>
public interface IAbstractSyntaxTree
{
    /// <summary>
    /// Корень
    /// </summary>
    IAbstractSyntaxTreeNode Root { get; }
}