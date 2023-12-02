// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var digits = CalibrationFinderRegex();
// https://regex101.com/r/1wW6Tq/1
var digitsAndWords = Calibrationv2FinderRegex();

var entries = File.ReadLines(args[0]);

var calibrationValues = entries.Select(GetCalibration);

Console.WriteLine("Calibration value:");
Console.WriteLine(calibrationValues.Sum());
Console.ReadKey();

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

int GetCalibration2(string entry)
{

}

partial class Program
{
    [GeneratedRegex(@"\D*(?<first>\d)?.*(?<last>\d)\D*")]
    private static partial Regex CalibrationFinderRegex();
    [GeneratedRegex(@"(?<first>one|two|three|four|five|six|seven|eight|nine|\d)?.*(?<last>one|two|three|four|five|six|seven|eight|nine|\d).*")]
    private static partial Regex Calibrationv2FinderRegex();
}