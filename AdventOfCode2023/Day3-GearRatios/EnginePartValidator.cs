using System.Buffers;

namespace Day3_GearRatios;

public class EnginePartValidator
{
    private readonly Coordinates Minimum = new(0, 0);
    private readonly Coordinates Maximum;
    private readonly char[,] m_engineMap;

    private const string InvalidChars = ".1234567890";

    public EnginePartValidator(IReadOnlyList<string> lines)
    {
        Maximum = new Coordinates(lines[0].Length, lines.Count);

        m_engineMap = new char[Maximum.X, Maximum.Y];
        InitMap(m_engineMap, lines.ToArray());
    }

    private static void InitMap(char[,] board, params string[] rows)
    {
        for (var i = 0; i < rows.Length; i++)
            for (var j = 0; j < rows[i].Length; j++)
                board[i, j] = rows[i][j];
    }

    public IEnumerable<Part> FilterValidParts(IEnumerable<Part> parts)
    {
        foreach (var part in parts)
        {
            var (bottom, top) = GetBoundaries(part.Position, part.PartLength);

            for (var i = bottom.X; i < top.X; i++)
                for (var j = bottom.Y; j < top.Y; i++)
                    if (!InvalidChars.Contains(m_engineMap[i, j]))
                        yield return part;
        }
    }

    private (Coordinates BottomLeft, Coordinates TopRight) GetBoundaries(Coordinates start, int length)
    {
        var botX = Math.Min(Minimum.X, start.X - 1);
        var botY = Math.Min(Minimum.Y, start.Y - 1);

        var topX = Math.Max(Maximum.X, start.X + length);
        var topY = Math.Max(Maximum.Y, start.Y + 1);

        return (new Coordinates(botX, botY), new Coordinates(topX, topY));
    }
}