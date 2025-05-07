namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Блока кода
/// </summary>
[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class StatementsBlock : Statement
{
    public IReadOnlyList<StatementListItem> Statements { get; }
    
    public StatementsBlock(IEnumerable<StatementListItem> statements)
    {
        Statements = statements.ToList();
        
        foreach (var statement in Statements)
            statement.Parent = this;
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => Statements;
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "{}";
}