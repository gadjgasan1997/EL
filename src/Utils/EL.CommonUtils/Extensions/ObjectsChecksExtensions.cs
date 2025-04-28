using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace EL.CommonUtils.Extensions;

/// <summary>
/// Методы расширения для валидации объектов
/// </summary>
public static class ObjectsChecksExtensions
{
    /// <summary>
    /// Проверяет элемент на null
    /// </summary>
    /// <param name="value">Элемент</param>
    /// <param name="errorMessage">Необязательное сообщение об ошибке</param>
    /// <param name="parameterName">Необязательное название свойства</param>
    /// <typeparam name="T">Тип элемента</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T CheckNotNull<T>(
        [NotNull] this T? value,
        string? errorMessage = null,
        [CallerArgumentExpression("value")] string? parameterName = null)
    {
        if (value is not null)
            return value;
       
        errorMessage ??= GetErrorMessage(parameterName);
        throw new ArgumentNullException(parameterName, errorMessage);
    }
    
    /// <summary>
    /// Проверяет строку на null или пустоту
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="errorMessage">Необязательное сообщение об ошибке</param>
    /// <param name="parameterName">Необязательное название свойства</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string CheckNotNullOrEmpty(
        [NotNull] this string? value,
        string? errorMessage = null,
        [CallerArgumentExpression("value")] string? parameterName = null)
    {
        errorMessage ??= GetErrorMessage(parameterName);
        
        if (value is null)
            throw new ArgumentNullException(parameterName, errorMessage);
        
        if (value.IsWhiteSpace())
            throw new ArgumentException(errorMessage, parameterName);

        return value;
    }

    private static string GetErrorMessage(string? parameterName) => $"Параметр '{parameterName}' должен быть заполнен";
}