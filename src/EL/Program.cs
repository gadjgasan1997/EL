using EL;
using EL.Compiler;
using EL.Extensions;
using EL.CommonUtils.Extensions;
using EL.Domain.Share.Dictionaries;
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
            
            var compiledAssemblyName = result
                .GetValue(command.CompiledAssemblyName)
                .CheckNotNullOrEmpty(nameof(command.CompiledAssemblyName));
            
            var compiledAssemblyOutputPath = result.GetValue(command.CompiledAssemblyOutputPath);
            var compileInParallel = result.GetValue(command.CompileInParallel);
            
            var outputTypeString = result.GetValue(command.OutputType);
            var outputType = outputTypeString == "Console" ? FileExtension.Executable : FileExtension.Library;
            
            using var serviceProvider = GetServiceProvider();
            var compiler = serviceProvider.GetRequiredService<ICompiler>();
            return compiler.Compile(
                projectDirectory,
                filesRelativePaths,
                compiledAssemblyName,
                compiledAssemblyOutputPath,
                compileInParallel,
                outputType);
        });
        
        return command;
    }
    
    private static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddServices();
        return services.BuildServiceProvider();
    }
}