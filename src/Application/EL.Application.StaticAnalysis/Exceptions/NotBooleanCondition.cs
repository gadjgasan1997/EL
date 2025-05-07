using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Выражение условия if возвращает тип отличный от bool
/// </summary>
public class NotBooleanCondition : SemanticException
{
    internal NotBooleanCondition(Statement statement, ElType type)
        : base(
            $"Условие имеет тип данных, отличный от bool. Тип данных: '{type}'. " +
            "Нельзя применить данное выражение в качестве условия")
    { }
    
    internal NotBooleanCondition(Expression expression, ElType type)
        : base(
            $"Условие имеет тип данных, отличный от bool. Тип данных: '{type}'. " +
            "Нельзя применить данное выражение в качестве условия")
    { }
}