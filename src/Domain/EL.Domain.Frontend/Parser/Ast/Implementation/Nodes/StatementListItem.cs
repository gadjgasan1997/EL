using static EL.Domain.Share.Dictionaries.TokenTypes;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

public abstract class StatementListItem : AbstractSyntaxTreeNode
{
    /// <summary>
    /// Признак необходимости отделять конструкцию от последующих с помощью <see cref="SemicolonTag" />
    /// </summary>
    public abstract bool NeedSemicolon { get; }
}

public abstract class Statement :
    StatementListItem;

public abstract class Declaration :
    StatementListItem;