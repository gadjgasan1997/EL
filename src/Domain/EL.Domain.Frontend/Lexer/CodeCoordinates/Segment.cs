namespace EL.Domain.Frontend.Lexer.CodeCoordinates;

/// <summary>
/// Сегмент кода
/// </summary>
/// <param name="Start">Начало</param>
/// <param name="End">Конец</param>
public record Segment(Coordinate Start, Coordinate End)
{
    /// <summary>
    /// Оператор преобразования сегмента в строку
    /// </summary>
    /// <param name="segment">Сегмент</param>
    /// <returns>Строка</returns>
    public static implicit operator string(Segment segment) =>
        segment.ToString();
    
    /// <summary>
    /// Оператор преобразования строки в сегмент
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Сегмент</returns>
    public static implicit operator Segment(string value)
    {
        var coords = value
            .Split("-")
            .Select(x => x[1..^1].Replace(" ", string.Empty))
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .ToArray();
        
        return new Segment(
            new Coordinate(coords[0][0], coords[0][1]),
            new Coordinate(coords[1][0], coords[1][1]));
    }
    
    /// <summary>
    /// Оператор сложения сегментов
    /// </summary>
    /// <param name="left">Сегмент</param>
    /// <param name="right">Сегмент</param>
    /// <returns>Сегмент</returns>
    public static Segment operator +(Segment left, Segment right) =>
        new(left.Start, right.End);
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => $"{Start}-{End}";
}