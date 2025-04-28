using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace EL;

[ExcludeFromCodeCoverage]
internal class ExecuteCommand : CliRootCommand
{
    internal ExecuteCommand() : base("EL Compiler")
    {
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
    /// Директория проекта
    /// </summary>
    internal CliArgument<string> ProjectDirectory { get; }
    
    /// <summary>
    /// Список относительных путей к файлам
    /// </summary>
    internal CliArgument<string> FilesRelativePaths { get; }    
}