namespace EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

public abstract class StatementListItem :
    AbstractSyntaxTreeNode;

public abstract class Statement :
    StatementListItem;

public abstract class Declaration :
    StatementListItem;