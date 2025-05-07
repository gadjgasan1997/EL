using EL.Domain.Share.Dictionaries;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Неподдерживаемая операция над типами
/// </summary>
public class UnsupportedTypesOperation : SemanticException
{
    internal UnsupportedTypesOperation(ElType leftType, Operator @operator, ElType rightType)
        : base($"Нельзя применить оператор '{@operator.Value}' к типу '{leftType}' и '{rightType}'")
    { }
}