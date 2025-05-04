using EL.Domain.Share.SeedWork;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Расширение файла
/// </summary>
public class FileExtension : Enumeration
{
    /// <summary>
    /// Файл на языке El
    /// </summary>
    public static FileExtension EL { get; } = new("el");
    
    /// <summary>
    /// Библиотека
    /// </summary>
    public static FileExtension Library { get; } = new("dll");
    
    /// <summary>
    /// Выполняемое приложение
    /// </summary>
    public static FileExtension Executable { get; } = new("exe");
    
    /// <summary>
    /// Конфиг
    /// </summary>
    public static FileExtension RuntimeConfig { get; } = new("runtimeconfig");
    
    private FileExtension(string value) : base(value)
    { }
}