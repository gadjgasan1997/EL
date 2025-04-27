using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace EL;

[ExcludeFromCodeCoverage]
internal class ExecuteCommand : CliRootCommand
{
    internal ExecuteCommand() : base("EL interpreter")
    {
        PathArgument = new CliOption<FileInfo>(name: "path")
        {
            Description = "Path to input file"
        };
        Add(PathArgument);
        
        DumpOption = new CliOption<bool>(name: "--dump", aliases: ["-d", "/d"])
        {
            Description = "Show dump data of interpreter",
            DefaultValueFactory = _ => false
        };
        Add(DumpOption);
    }
    
    internal CliOption<FileInfo> PathArgument { get; }
    
    internal CliOption<bool> DumpOption { get; }    
}