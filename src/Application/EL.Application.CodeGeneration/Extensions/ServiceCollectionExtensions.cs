using Microsoft.Extensions.DependencyInjection;
using EL.Application.CodeGeneration.Services.CodeGenerator;

namespace EL.Application.CodeGeneration.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы слоя приложения для кодогенерации
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static IServiceCollection AddCodeGenerationServices(this IServiceCollection services) =>
        services.AddSingleton<ICodeGenerator, CodeGenerator>();
}