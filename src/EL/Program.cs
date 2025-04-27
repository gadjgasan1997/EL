using EL;
using EL.Infrastructure.Extensions;
using EL.Infrastructure.Services;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

return CliParser.Parse(GetCommand(), args).Invoke();

[ExcludeFromCodeCoverage]
internal static partial class Program
{
    private static ExecuteCommand GetCommand()
    {
        ExecuteCommand command = new();
        command.SetAction(_ =>
        {
            using var serviceProvider = GetServiceProvider();
            var executor = serviceProvider.GetRequiredService<Executor>();
            return executor.Invoke();
        });
        
        return command;
    }
    
    private static ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices = null)
    {
        var services = new ServiceCollection();
        services
            .AddLogging()
            .AddInfrastructure();
        
        configureServices?.Invoke(services);
        return services.BuildServiceProvider();
    }
}