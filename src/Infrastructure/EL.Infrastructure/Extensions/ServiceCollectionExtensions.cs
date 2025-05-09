using System.IO.Abstractions;
using EL.Infrastructure.Services.Emitter;
using EL.Infrastructure.Services.SourceCodeProvider;
using Microsoft.Extensions.DependencyInjection;

namespace EL.Infrastructure.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы слоя инфраструктуры
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEmitter, DumpEmitter>();
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<ISourceCodeProvider, SourceCodeProvider>();
        return services;
    }
}