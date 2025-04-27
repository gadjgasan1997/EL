using System.Reflection;
using EL.Domain.Share.SeedWork;
using EL.Infrastructure.Services;
using EL.CommonUtils.Reflection.Extensions;
using EL.CommonUtils.Reflection.Helpers;
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
        var assemblies = AssemblyHelpers.GetAllAssembliesWithReferences().ToArray();
        services.RegisterDomainServices(assemblies);
        services.AddSingleton<Executor>();
        return services;
    }
    
    private static void RegisterDomainServices(this IServiceCollection services, Assembly[] assemblies)
    {
        var typeToFind = typeof(IDomainService);
        var domainServicesTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetTypeInfo().IsImplements(typeToFind))
            .ToList();
        
        foreach (var domainServiceType in domainServicesTypes)
        {
            var interfaceType = domainServiceType
                .GetInterfaces()
                .First(t => t.GetInterfaces().Contains(typeToFind));
            
            services.AddSingleton(interfaceType, domainServiceType);
        }
    }
}