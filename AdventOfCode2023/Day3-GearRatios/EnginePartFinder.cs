using System.Text.RegularExpressions;

namespace Day3_GearRatios;

public partial class EnginePartFinder
{
    private readonly IReadOnlyList<string> m_lines;
    private static readonly Regex FindPartNumbers = FindPartNumberRegex();

    public EnginePartFinder(IReadOnlyList<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        m_lines = lines;
    }

    public IEnumerable<Part> FindPossibleParts()
    {
        for (var lineNumber = 0; lineNumber < m_lines.Count; lineNumber++)
        {
            var parts = GetParts(m_lines[lineNumber]);

            foreach (var part in parts)
            {
                var position = GetPartPosition(m_lines[lineNumber], part);
                yield return new Part(part, new Coordinates(position.Index, lineNumber));
            }
        }
    }

    private static IEnumerable<string> GetParts(string entry)
    {
        var matches = FindPartNumbers.Matches(entry);

        foreach (Match match in matches)
            yield return match.Groups["PartNumber"].Value;
    }

    private static (int Index, int Length) GetPartPosition(string entry, string partNumber) =>
        (entry.IndexOf(partNumber), partNumber.Length);

    [GeneratedRegex(@"(?<PartNumber>\d+)")]
    private static partial Regex FindPartNumberRegex();
}