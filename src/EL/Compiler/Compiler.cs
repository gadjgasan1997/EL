using EL.Domain.Share.Dictionaries;

namespace EL.Compiler;

/// <inheritdoc cref="ICompiler" />
internal class Compiler : ICompiler
{
    /// <inheritdoc cref="ICompiler.Compile" />
    public int Compile(
        string projectDirectory,
        string[] filesRelativePaths,
        string compiledAssemblyName,
        string? compiledAssemblyOutputPath,
        bool? compileInParallel,
        FileExtension outputType)
    {
        return 0;
    }
}