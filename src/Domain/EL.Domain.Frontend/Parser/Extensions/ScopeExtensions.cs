using EL.CommonUtils.Extensions;

namespace EL.Domain.Frontend.Parser.Extensions;

/// <summary>
/// Методы расширения для <see cref="Scope" />
/// </summary>
public static class ScopeExtensions
{
    /// <summary>
    /// Проверяет, что область видимости была инициализирована
    /// </summary>
    /// <param name="scope">Область видимости</param>
    /// <returns>Область видимости</returns>
    public static Scope ShouldBeInitialized(this Scope? scope) =>
        scope.CheckNotNull("Область видимости не инициализирована");
}