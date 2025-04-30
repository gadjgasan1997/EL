using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace EL;

[ExcludeFromCodeCoverage]
internal class ExecuteCommand : CliRootCommand
{
    internal ExecuteCommand() : base("EL Compiler")
    {
        CompiledAssemblyName = new CliArgument<string>(name: "assemblyName")
        {
            Description = "Name of an assembly",
        };
        Add(CompiledAssemblyName);
        
        CompiledAssemblyOutputPath = new CliArgument<string>(name: "assemblyOutputPath")
        {
            Description = "Output path for generated assembly"
        };
        Add(CompiledAssemblyOutputPath);
        
        ProjectDirectory = new CliArgument<string>(name: "directory")
        {
            Description = "Directory of the project"
        };
        Add(ProjectDirectory);
        
        FilesRelativePaths = new CliArgument<string>(name: "files")
        {
            Description = "Files in the project"
        };
        Add(FilesRelativePaths);
    }
    
    /// <summary>
    /// Название компилируемой сборки
    /// </summary>
    internal CliArgument<string> CompiledAssemblyName { get; }
    
    /// <summary>
    /// Путь для файлов компилируемой сборки
    /// </summary>
    internal CliArgument<string> CompiledAssemblyOutputPath { get; }
    
    /// <summary>
    /// Директория проекта
    /// </summary>
    internal CliArgument<string> ProjectDirectory { get; }
    
    /// <summary>
    /// Список относительных путей к файлам
    /// </summary>
    internal CliArgument<string> FilesRelativePaths { get; }    
}