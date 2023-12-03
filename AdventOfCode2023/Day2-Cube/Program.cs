// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var rules = new Dictionary<string, int>
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 }
};

var gameRegex = GetGameIdRegex();
var drawRegex = GetDrawRegex();

var entries = File.ReadLines(args[0]);
var sum = 0;
var powerSum = 0;

foreach (var entry in entries)
{
    var gameId = GetGameId(entry);
    var draws = GetDraws(entry).ToList();

    if (IsCompliant(draws))
    {
        sum += gameId;
    }

    powerSum += GetPower(draws);
}

Console.WriteLine("Sum of the game IDs:");
Console.WriteLine(sum);

Console.WriteLine("Sum of powers:");
Console.WriteLine(powerSum);
return;

int GetGameId(string entry)
{
    return int.Parse(gameRegex.Match(entry).Groups["game"].Value);
}

IEnumerable<(string color, int amount)> GetDraws(string entry)
{
    // Extract the draw section of the string
    var drawStr = entry.Split(':')[1].Trim();

    // Get all matches for draw regex in draw string
    var matches = drawRegex.Matches(drawStr);
    foreach (Match match in matches)
    {
        yield return (match.Groups["color"].Value, int.Parse(match.Groups["quantity"].Value));
    }
}

bool IsCompliant(IEnumerable<(string color, int amount)> draws)
{
    return draws.All(draw => draw.amount <= rules[draw.color]);
}

int GetPower(IEnumerable<(string color, int amount)> draws)
{
    var maxAmountPerColor = new Dictionary<string, int>
    {
        { "red", 1},
        { "green", 1 },
        { "blue", 1 }
    };

    foreach (var draw in draws)
    {
        maxAmountPerColor[draw.color] = Math.Max(maxAmountPerColor[draw.color], draw.amount);
    }

    return maxAmountPerColor.Values.Aggregate(1, (i, j) => i*j);
}

partial class Program
{
    [GeneratedRegex(@"Game\s(?<game>\d+)")]
    private static partial Regex GetGameIdRegex();

    [GeneratedRegex(@"(?<quantity>\d+)\s(?<color>\w+)")]
    private static partial Regex GetDrawRegex();
}