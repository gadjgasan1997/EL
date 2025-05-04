namespace EL.Infrastructure.Services.SourceCodeProvider;

/// <summary>
/// Файл с исходным кодом
/// </summary>
/// <param name="FilePath">Путь к файлу</param>
/// <param name="SourceCode">Исходный код</param>
public record SourceCodeFile(string FilePath, string SourceCode);