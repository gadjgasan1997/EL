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
        
        CompiledAssemblyName = new CliArgument<string>(name: "assemblyName")
        {
            Description = "Name of an assembly"
        };
        Add(CompiledAssemblyName);
        
        CompiledAssemblyOutputPath = new CliOption<string>(name: "--assemblyOutputPath")
        {
            Description = "Output path for generated assembly"
        };
        Add(CompiledAssemblyOutputPath);
        
        CompileInParallel = new CliOption<bool?>(name: "--compileInParallel")
        {
            Description = "Need compile files in parallel"
        };
        Add(CompileInParallel);
        
        OutputType = new CliOption<string?>(name: "--outputType")
        {
            Description = "Output type of generated assembly"
        };
        Add(OutputType);
    }
    
    /// <summary>
    /// Директория проекта
    /// </summary>
    internal CliArgument<string> ProjectDirectory { get; }
    
    /// <summary>
    /// Список относительных путей к файлам
    /// </summary>
    internal CliArgument<string> FilesRelativePaths { get; }    
    
    /// <summary>
    /// Название компилируемой сборки
    /// </summary>
    internal CliArgument<string> CompiledAssemblyName { get; }
    
    /// <summary>
    /// Путь для файлов компилируемой сборки
    /// </summary>
    internal CliOption<string> CompiledAssemblyOutputPath { get; }
    
    /// <summary>
    /// Признак необходимости компилировать файлы параллельно
    /// </summary>
    internal CliOption<bool?> CompileInParallel { get; }
    
    /// <summary>
    /// Тип выходного файла
    /// </summary>
    internal CliOption<string?> OutputType { get; }
}