namespace EL.Infrastructure.Services.Executor;

/// <summary>
/// Сервис, выполняющий обработку действия из командной строки
/// </summary>
public interface IExecutor
{
    /// <summary>
    /// Выполняет обработку действия
    /// </summary>
    /// <param name="projectDirectory">Директория проекта</param>
    /// <param name="filesRelativePaths">Список относительных путей к файлам</param>
    /// <returns>Результат обработки действия</returns>
    int Invoke(string projectDirectory, string[] filesRelativePaths);
}