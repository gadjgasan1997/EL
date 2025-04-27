using System.Reflection;

namespace EL.CommonUtils.Reflection.Extensions;

/// <summary>
/// Методы расширения для <see cref="TypeInfo" />
/// </summary>
public static class TypeInfoExtensions
{
    /// <summary>
    /// Определяет, что тип реализует определенный интерфейс
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <param name="implementedInterfaceType">Тип интерфейса</param>
    /// <returns></returns>
    public static bool IsImplements(this TypeInfo typeInfo, Type implementedInterfaceType) =>
        !typeInfo.IsAbstract && typeInfo.ImplementedInterfaces.Any(type => type == implementedInterfaceType);
}