using EL.Domain.Share.Dictionaries;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Неподдерживаемая операция над типом
/// </summary>
public class UnsupportedTypeOperation : SemanticException
{
    internal UnsupportedTypeOperation(ElType type, Operator @operator)
        : base($"Нельзя применить оператор '{@operator.Value}' к типу '{type}'")
    { }
}