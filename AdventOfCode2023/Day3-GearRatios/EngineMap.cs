using System.Text.RegularExpressions;

namespace Day3_GearRatios;

public partial class EngineMap(IReadOnlyList<string> lines)
{
    private static readonly Regex FindPartNumbers = FindPartNumberRegex();

    public IEnumerable<Part> FindPossibleParts()
    {
        for (var lineNumber = 0; lineNumber < lines.Count; lineNumber++)
        {
            var parts = GetParts(lines[lineNumber]);

            foreach (var part in parts)
            {
                var position = GetPartPosition(lines[lineNumber], part);
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

    private static (int Index, int Length) GetPartPosition(string entry, string partNumber)
    {
        return (entry.IndexOf(partNumber), partNumber.Length-1);
    }

    [GeneratedRegex(@"(?<PartNumber>\d+)")]
    private static partial Regex FindPartNumberRegex();
}