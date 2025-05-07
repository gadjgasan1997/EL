using EL.CommonUtils.Extensions;
using EL.Domain.Frontend.Parser.Ast;

namespace EL.Domain.Frontend.Parser.Extensions;

/// <summary>
/// Методы расширения для <see cref="IAbstractSyntaxTreeNode"/>
/// </summary>
public static class AbstractSyntaxTreeNodeExtensions
{
    /// <summary>
    /// Возвращает ближайщую ноду по типу
    /// </summary>
    /// <typeparam name="TNode">Тип ноды</typeparam>
    /// <returns>Нода</returns>
    public static TNode GetRequiredClosestNode<TNode>(this IAbstractSyntaxTreeNode node)
        where TNode : class, IAbstractSyntaxTreeNode
    {
        return node
            .GetClosestNode<TNode>()
            .CheckNotNull(
                $"Ближайщая нода с типом '{typeof(TNode).FullName}' не найдена",
                "Parent");
    }
}