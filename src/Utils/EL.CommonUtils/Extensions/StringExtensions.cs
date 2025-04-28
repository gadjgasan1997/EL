namespace EL.CommonUtils.Extensions;

/// <summary>
/// Методы расширения для строк
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Проверяет что строка состоит из пробелов
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns></returns>
    public static bool IsWhiteSpace(this string value) => value.All(char.IsWhiteSpace);
}