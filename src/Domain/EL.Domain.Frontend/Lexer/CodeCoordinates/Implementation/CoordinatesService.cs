using System.Buffers;
using static System.Environment;

namespace EL.Domain.Frontend.Lexer.CodeCoordinates.Implementation;

/// <inheritdoc cref="ICoordinatesService" />
internal class CoordinatesService : ICoordinatesService
{
    private readonly SearchValues<char> _searchValues = SearchValues.Create('\n');
    
    /// <inheritdoc cref="ICoordinatesService.GetLines" />
    public IReadOnlyList<int> GetLines(string text)
    {
        var newText = text.EndsWith(NewLine) ? text : text + NewLine;
        var textLength = newText.Length;
        
        var indices = new List<int>(capacity: 128) { -1 };
        var textAsSpan = newText.AsSpan();
        while (true)
        {
            var start = indices[^1] + 1;
            if (start == textLength)
                break;
            
            var index = textAsSpan
                .Slice(start, length: textLength - start)
                .IndexOfAny(_searchValues);
            
            indices.Add(start + index);
        }
        
        return indices.ToList();
    }
    
    /// <inheritdoc cref="ICoordinatesService.GetCoordinate" />
    public Coordinate GetCoordinate(int absoluteIndex, IReadOnlyList<int> newLineList)
    {
        for (var i = 1; i < newLineList.Count; i++)
        {
            if (absoluteIndex > newLineList[i])
                continue;
            
            var offset = newLineList[i - 1];
            return new Coordinate(Line: i, Column: absoluteIndex - offset);
        }
        
        return Coordinate.Start;
    }
}