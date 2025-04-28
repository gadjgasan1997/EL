namespace EL.Infrastructure.Services.SourceCodeProvider;

/// <summary>
/// Провайдер получения исходного кода
/// </summary>
internal interface ISourceCodeProvider
{
    /// <summary>
    /// Возвращает содержимое файлов
    /// </summary>
    /// <param name="projectDirectory">Директория проекта</param>
    /// <param name="filesRelativePaths">Список относительных путей к файлам</param>
    /// <returns></returns>
    IEnumerable<string> GetFilesContent(string projectDirectory, string[] filesRelativePaths);
}