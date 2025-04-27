using System.Reflection;

namespace EL.CommonUtils.Extensions;

/// <summary>
/// Методы расширения для <see cref="Assembly" />
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Загружает список сборок, на которые ссылается текущая
    /// </summary>
    /// <param name="assembly">Текущая сборка</param>
    /// <returns>Загруженнный список сборок</returns>
    public static List<Assembly> LoadReferencedAssemblies(this Assembly assembly) =>
        assembly
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .ToList();
}