using EL.Domain.Share.Dictionaries;

namespace EL.Compiler;

/// <summary>
/// Сервис компиляции проектов
/// </summary>
public interface ICompiler
{
    /// <summary>
    /// Компилирует проект
    /// </summary>
    /// <param name="projectDirectory">Директория проекта</param>
    /// <param name="filesRelativePaths">Список относительных путей к файлам</param>
    /// <param name="compiledAssemblyName">Название компилируемой сборки</param>
    /// <param name="compiledAssemblyOutputPath">Путь для файлов компилируемой сборки</param>
    /// <param name="compileInParallel">Признак необходимости компилировать файлы параллельно</param>
    /// <param name="outputType">Тип выходного файла</param>
    /// <returns>Результат обработки действия</returns>
    int Compile(
        string projectDirectory,
        string[] filesRelativePaths,
        string compiledAssemblyName,
        string? compiledAssemblyOutputPath,
        bool? compileInParallel,
        FileExtension outputType);
}