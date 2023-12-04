// See https://aka.ms/new-console-template for more information

using Day3_GearRatios;

var entries = File.ReadLines(args[0]);

var finder = new EnginePartFinder(entries.ToList());
var parts = finder.FindPossibleParts();

var validator = new EnginePartValidator(entries.ToList());
var validParts = validator.FilterValidParts(parts);

var partsNumberSum = validParts.Sum(p => p.PartValue);

Console.WriteLine("Sum of valid parts:");
Console.WriteLine(partsNumberSum);