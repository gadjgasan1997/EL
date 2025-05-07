using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

/// <summary>
/// Тип языка El
/// </summary>
public abstract record ElTypeNodeBase : IVisitable<ElTypeNodeBase>
{
    /// <summary>
    /// Область видимости
    /// </summary>
    public Scope? Scope { get; set; }
    
    public abstract TReturn Accept<TReturn>(IVisitor<ElTypeNodeBase, TReturn> visitor);
}

/// <summary>
/// Тип языка El
/// </summary>
/// <param name="Id">Id</param>
[AutoVisitable<ElTypeNodeBase>]
public partial record ElTypeNode(IdentifierExpression Id) : ElTypeNodeBase
{
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => Id;
}