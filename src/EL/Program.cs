using EL;
using EL.CommonUtils.Extensions;
using EL.Infrastructure.Extensions;
using EL.Infrastructure.Services.Executor;
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
        command.SetAction(result =>
        {
            var projectDirectory = result
                .GetValue(command.ProjectDirectory)
                .CheckNotNullOrEmpty(nameof(command.ProjectDirectory));
            
            var filesRelativePathsString = result
                .GetValue(command.FilesRelativePaths)
                .CheckNotNullOrEmpty(nameof(command.FilesRelativePaths));
            
            var filesRelativePaths = filesRelativePathsString.Split(",");
            
            using var serviceProvider = GetServiceProvider();
            var executor = serviceProvider.GetRequiredService<IExecutor>();
            return executor.Invoke(projectDirectory, filesRelativePaths);
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