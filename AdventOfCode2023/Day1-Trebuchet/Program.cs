// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var conversionMapper = new Dictionary<string, string>
{
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6" },
    { "seven", "7" },
    { "eight", "8" },
    { "nine", "9" },
};

var digits = CalibrationFinderRegex();
var digitsAndWords = Calibrationv2FinderRegex();

var entries = File.ReadLines(args[0]);

var calibrationValues = entries.Select(GetCalibration);
var calibrationV2Values = entries.Select(GetCalibrationWithWordConversion);

Console.WriteLine("Calibration value version 1:");
Console.WriteLine(calibrationValues.Sum());

Console.WriteLine("Calibration value version 2:");
Console.WriteLine(calibrationV2Values.Sum());

return;

// Get calibration value from string entry
// If only last is found, then return calibration of twice the last
int GetCalibration(string entry)
{
    var match = digits.Match(entry);
    var numberStr = match.Groups["first"].Value + match.Groups["last"].Value;

    if (numberStr.Length == 1)
        numberStr += numberStr;

    return int.TryParse(numberStr, out var calibration) ? calibration : 0;
}

int GetCalibrationWithWordConversion(string entry)
{
    var match = digitsAndWords.Match(entry);

    var firstValue = match.Groups["first"].Value;
    var lastValue = match.Groups["last"].Value;

    var first = conversionMapper.TryGetValue(firstValue, out var v) ? v : firstValue;
    var last = conversionMapper.TryGetValue(lastValue, out var w) ? w : lastValue;

    var numberStr = first + last;

    if (numberStr.Length == 1)
        numberStr += numberStr;

    return int.TryParse(numberStr, out var calibration) ? calibration : 0;
}

partial class Program
{
    [GeneratedRegex(@"\D*(?<first>\d)?.*(?<last>\d)\D*")]
    private static partial Regex CalibrationFinderRegex();

    //https://regex101.com/r/nt98t2/1
    [GeneratedRegex(@"(?<first>one|two|three|four|five|six|seven|eight|nine|\d)(.*(?<last>one|two|three|four|five|six|seven|eight|nine|\d))?")]
    private static partial Regex Calibrationv2FinderRegex();
}

