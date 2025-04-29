namespace EL.Domain.Frontend.Lexer.CodeCoordinates;

/// <summary>
/// Координата
/// </summary>
/// <param name="Line">Строка</param>
/// <param name="Column">Колонка</param>
public record Coordinate(int Line, int Column)
{
    private Coordinate() : this(Line: 1, Column: 1)
    { }
    
    /// <summary>
    /// Координаты начала файла
    /// </summary>
    public static Coordinate Start => new();
    
    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => $"({Line}, {Column})";
}