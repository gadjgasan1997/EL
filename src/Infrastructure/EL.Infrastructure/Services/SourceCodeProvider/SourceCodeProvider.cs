using System.IO.Abstractions;
using EL.CommonUtils.Extensions;
using EL.Domain.Share.Dictionaries;

namespace EL.Infrastructure.Services.SourceCodeProvider;

/// <inheritdoc cref="ISourceCodeProvider" />
internal class SourceCodeProvider(IFileSystem fileSystem) : ISourceCodeProvider
{
    private const string ALL_FILES = "<all_files>";
    
    /// <inheritdoc cref="ISourceCodeProvider.GetFilesContent" />
    public IEnumerable<SourceCodeFile> GetFilesContent(
        string projectDirectory,
        string[] filesRelativePaths)
    {
        projectDirectory.CheckNotNullOrEmpty();
        
        if (filesRelativePaths is [ALL_FILES])
        {
            var fileMask = $"*.{FileExtension.EL.Value}";
            foreach (var path in Directory.GetFiles(projectDirectory, fileMask))
                yield return new SourceCodeFile(path, fileSystem.File.ReadAllText(path));
            
            yield break;
        }
        
        foreach (var path in filesRelativePaths)
        {
            path.CheckNotNullOrEmpty();
            
            var inputFilePath = Path.Combine(projectDirectory, path);
            yield return new SourceCodeFile(path, fileSystem.File.ReadAllText(inputFilePath));
        }
    }
}