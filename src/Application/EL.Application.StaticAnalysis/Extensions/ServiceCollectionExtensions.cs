using Microsoft.Extensions.DependencyInjection;

namespace EL.Application.StaticAnalysis.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы слоя приложения для статического анализа
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static IServiceCollection AddStaticAnalysisServices(this IServiceCollection services)
    {
        return services;
    }
}