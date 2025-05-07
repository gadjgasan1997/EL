using EL.Domain.Share.Dictionaries;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Несопоставимые типы данных при присвоении значения
/// </summary>
public class AssigmentIncomparableTypes : SemanticException
{
    internal AssigmentIncomparableTypes(ElType destinationType, ElType sourceType)
        : base($"Значение с типом '{sourceType}' нельзя присвоить переменной с типом '{destinationType}'")
    { }
}