using EL.Domain.IR.Symbols;

namespace EL.Application.StaticAnalysis.Exceptions;

/// <summary>
/// Переменная должна быть проинициализирована при ее использовании
/// </summary>
public class VariableShouldBeInitializedBeforeUsage : SemanticException
{
    internal VariableShouldBeInitializedBeforeUsage(VariableSymbol symbol)
        : base($"Переменная должна быть проинициализирована при ее использовании. Переменная: '{symbol.Name}'")
    { }
}