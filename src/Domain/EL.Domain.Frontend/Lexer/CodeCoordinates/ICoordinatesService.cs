using EL.Domain.Share.SeedWork;

namespace EL.Domain.Frontend.Lexer.CodeCoordinates;

/// <summary>
/// Сервис получения координат кода
/// </summary>
internal interface ICoordinatesService : IDomainService
{
    /// <summary>
    /// Возвращает список индексов переноса строки внутри фрагмента исходного кода<br/>
    /// Всегда начинается с -1
    /// </summary>
    public IReadOnlyList<int> GetLines(string text);
    
    /// <summary>
    /// Возвращает координату
    /// </summary>
    /// <param name="absoluteIndex">Индекс символа от начала строки, в диапазоне [0, ДлинаСтроки)</param>
    /// <param name="newLineList">Список индексов переноса строки <see cref="GetLines"/></param>
    /// <returns>Координата</returns>
    public Coordinate GetCoordinate(
        int absoluteIndex,
        IReadOnlyList<int> newLineList);
}