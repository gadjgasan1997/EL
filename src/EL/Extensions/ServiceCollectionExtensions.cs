using System.Reflection;
using EL.Compiler;
using EL.Domain.Share.SeedWork;
using EL.CommonUtils.Reflection.Helpers;
using EL.CommonUtils.Reflection.Extensions;
using EL.Infrastructure.Extensions;
using EL.Application.CodeGeneration.Extensions;
using EL.Application.StaticAnalysis.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EL.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var assemblies = AssemblyHelpers.GetAllAssembliesWithReferences().ToArray();
        services.RegisterDomainServices(assemblies);
        
        return services
            .AddSingleton<ICompiler, DumpCompiler>()
            .AddLogging(builder => builder.ClearProviders().AddConsole())
            .AddStaticAnalysisServices()
            .AddCodeGenerationServices()
            .AddInfrastructure();
    }
    
    private static void RegisterDomainServices(this IServiceCollection services, Assembly[] assemblies)
    {
        services
            .RegisterServices(assemblies, typeof(ISingletonService), ServiceLifetime.Singleton)
            .RegisterServices(assemblies, typeof(ITransientService), ServiceLifetime.Transient);
    }
    
    private static IServiceCollection RegisterServices(
        this IServiceCollection services,
        Assembly[] assemblies,
        Type typeToFind,
        ServiceLifetime lifetime)
    {
        var domainServicesTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetTypeInfo().IsImplements(typeToFind))
            .ToList();
        
        foreach (var domainServiceType in domainServicesTypes)
        {
            var interfaceType = domainServiceType
                .GetInterfaces()
                .First(type => type.GetInterfaces().Contains(typeToFind));
            
            services.Add(ServiceDescriptor.Describe(interfaceType, domainServiceType, lifetime));
        }
        
        return services;
    }
}