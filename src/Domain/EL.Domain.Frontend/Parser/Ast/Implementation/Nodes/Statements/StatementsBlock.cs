namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;

/// <summary>
/// Блока кода
/// </summary>
public class StatementsBlock : Statement
{
    private readonly List<StatementListItem> _statements;
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.Children" />
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children => _statements;
    
    public StatementsBlock(IEnumerable<StatementListItem> statements)
    {
        _statements = statements.ToList();
        _statements.ForEach(item => item.Parent = this);
    }
    
    /// <inheritdoc cref="StatementListItem.NeedSemicolon" />
    public override bool NeedSemicolon => true;
    
    /// <inheritdoc cref="Statement.NodeRepresentation" />
    protected override string NodeRepresentation() => "{}";
}