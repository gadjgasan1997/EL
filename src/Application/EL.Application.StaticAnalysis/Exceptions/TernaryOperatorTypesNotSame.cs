using EL.Domain.Share.Dictionaries;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Типы, возвращаемые тернарным выражения не совпадают
/// </summary>
public class TernaryOperatorTypesNotSame : SemanticException
{
    internal TernaryOperatorTypesNotSame(ElType leftType, ElType rightType)
        : base(
            "Типы, возвращаемые тернарным выражения не совпадают. " +
            $"Тип левой части: '{leftType}'. Тип правой части: '{rightType}'")
    { }
}