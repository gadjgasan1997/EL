namespace EL.CommonUtils.Collections.Extensions;

/// <summary>
/// Методы расширения для коллекций
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Проходится по коллекции дополнительно проверяя ее на наличие дубликатов
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="action">Действие, которое требуется выполнить над элементом</param>
    /// <param name="onDuplicate">Действие, которое требуется выполнить при обнаружении дубликата</param>
    /// <typeparam name="T">Тип элемента</typeparam>
    public static void ForEachWithDuplicatesCheck<T>(
        this IEnumerable<T> enumerable,
        Action<T> action,
        Action<T> onDuplicate)
    {
        enumerable.ForEachWithDuplicatesCheckCore(EqualityComparer<T>.Default, action, onDuplicate);
    }
    
    /// <summary>
    /// Проходится по коллекции дополнительно проверяя ее на наличие дубликатов
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="comparer">Комперер</param>
    /// <param name="action">Действие, которое требуется выполнить над элементом</param>
    /// <param name="onDuplicate">Действие, которое требуется выполнить при обнаружении дубликата</param>
    /// <typeparam name="T">Тип элемента</typeparam>
    public static void ForEachWithDuplicatesCheck<T>(
        this IEnumerable<T> enumerable,
        IEqualityComparer<T> comparer,
        Action<T> action,
        Action<T> onDuplicate)
    {
        enumerable.ForEachWithDuplicatesCheckCore(comparer, action, onDuplicate);
    }
    
    /// <summary>
    /// Проходится по коллекции дополнительно проверяя ее на наличие дубликатов
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="comparer">Комперер</param>
    /// <param name="action">Действие, которое требуется выполнить над элементом</param>
    /// <param name="onDuplicate">Действие, которое требуется выполнить при обнаружении дубликата</param>
    /// <typeparam name="T">Тип элемента</typeparam>
    private static void ForEachWithDuplicatesCheckCore<T>(
        this IEnumerable<T> enumerable,
        IEqualityComparer<T> comparer,
        Action<T> action,
        Action<T> onDuplicate)
    {
        var set = new HashSet<T>(comparer);
        foreach (var item in enumerable)
        {
            if (item is null || set.Add(item))
            {
                action(item);
                continue;
            }
            
            onDuplicate(item);
        }
    }
}