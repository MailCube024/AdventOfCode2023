namespace Day3_GearRatios;

public class EnginePartValidator
{
    private readonly Coordinates Minimum = new(0, 0);
    private readonly Coordinates Maximum;
    private readonly char[,] m_engineMap;

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
}