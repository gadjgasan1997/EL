using EL.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EL.Infrastructure.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы уровня инфраструктуры
    /// </summary>
    /// <param name="services"></param>
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<Executor>();
    }
}