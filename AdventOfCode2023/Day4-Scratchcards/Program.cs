// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var getScratchCard = GetScratchCardRegex();

var entries = File.ReadLines(args[0]).ToList();

var sumOfCards = 0;
var cardsCount = Enumerable.Repeat(1, entries.Count).ToArray();

for (var i = 0; i < entries.Count; i++)
{
    var entry = entries[i];
    var split = entry.Split(":");
    var match = getScratchCard.Match(split[1]);

    var winnings = match.Groups["winningNumbers"].Captures.Select(c => int.Parse(c.Value)).ToHashSet();
    var numbers = match.Groups["drawnNumbers"].Captures.Select(c => int.Parse(c.Value)).ToHashSet();

    var power = numbers.Count(winnings.Contains);

    if (power > 0)
    {
        sumOfCards += (int)Math.Pow(2, power - 1);

        for (var card = 0; card < cardsCount[i]; card++)
            for (var j = 0; j < power; j++)
                cardsCount[i + j + 1] += 1;
    }
}

Console.WriteLine("Sum of the winning cards:");
Console.WriteLine(sumOfCards);

Console.WriteLine("Sum of won scratch cards:");
Console.WriteLine(cardsCount.Sum());

partial class Program
{
    [GeneratedRegex(@"(?<winningNumbers>\s*\d+)+\s\|(?<drawnNumbers>\s*\d+)+")]
    private static partial Regex GetScratchCardRegex();
}