using System.IO.Abstractions;
using EL.CommonUtils.Extensions;

namespace EL.Infrastructure.Services.SourceCodeProvider;

/// <inheritdoc cref="ISourceCodeProvider" />
internal class SourceCodeProvider(IFileSystem fileSystem) : ISourceCodeProvider
{
    /// <inheritdoc cref="ISourceCodeProvider.GetFilesContent" />
    public IEnumerable<string> GetFilesContent(
        string projectDirectory,
        string[] filesRelativePaths)
    {
        projectDirectory.CheckNotNullOrEmpty();
        
        foreach (var fileRelativePath in filesRelativePaths)
        {
            fileRelativePath.CheckNotNullOrEmpty();
            
            var inputFilePath = Path.Combine(projectDirectory, fileRelativePath);
            yield return fileSystem.File.ReadAllText(inputFilePath);
        }
    }
}