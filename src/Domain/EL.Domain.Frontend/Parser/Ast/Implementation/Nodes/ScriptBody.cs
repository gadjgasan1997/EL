using Visitor.NET;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

[AutoVisitable<IAbstractSyntaxTreeNode>]
public partial class ScriptBody : AbstractSyntaxTreeNode
{
    private readonly List<StatementListItem> _statementList;
    
    protected override IReadOnlyList<IAbstractSyntaxTreeNode> Children =>
        _statementList;
    
    public ScriptBody(IEnumerable<StatementListItem> statementList)
    {
        _statementList = new List<StatementListItem>(statementList);
        _statementList.ForEach(item => item.Parent = this);
    }
    
    /// <inheritdoc cref="AbstractSyntaxTreeNode.NodeRepresentation" />
    protected override string NodeRepresentation() => "Script";
}