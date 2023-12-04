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
        Maximum = new Coordinates(lines[0].Length - 1, lines.Count - 1);

        m_engineMap = new char[Maximum.X + 1, Maximum.Y + 1];
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
        var validParts = new List<Part>();

        foreach (var part in parts)
        {
            var (bottom, top) = GetBoundaries(part.Position, part.PartLength);

            for (var x = bottom.X; x <= top.X; x++)
                for (var y = bottom.Y; y <= top.Y; y++)
                    // If any characters are not invalid, then we have a valid part and we should return it.
                    if (!InvalidChars.Contains(m_engineMap[y, x]))
                    {
                        validParts.Add(part);
                        goto next;
                    }

            next: ;
        }

        Console.WriteLine(validParts.Aggregate("", (s, part) => s + part.PartNumber + Environment.NewLine));
        return validParts;
    }

    private (Coordinates BottomLeft, Coordinates TopRight) GetBoundaries(Coordinates start, int length)
    {
        var botX = Math.Max(Minimum.X, start.X - 1);
        var botY = Math.Max(Minimum.Y, start.Y - 1);

        var topX = Math.Min(Maximum.X, start.X + length);
        var topY = Math.Min(Maximum.Y, start.Y + 1);

        return (new Coordinates(botX, botY), new Coordinates(topX, topY));
    }
}