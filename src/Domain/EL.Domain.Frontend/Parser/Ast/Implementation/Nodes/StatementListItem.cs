using static EL.Domain.Share.Dictionaries.TokenType;

namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

public abstract class StatementListItem : AbstractSyntaxTreeNode
{
    /// <summary>
    /// Признак необходимости отделять конструкцию от последующих с помощью <see cref="Semicolon" />
    /// </summary>
    public abstract bool NeedSemicolon { get; }
}

public abstract class Statement :
    StatementListItem;

public abstract class Declaration :
    StatementListItem;