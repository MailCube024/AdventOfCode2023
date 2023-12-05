// See https://aka.ms/new-console-template for more information

using Day3_GearRatios;

var entries = File.ReadLines(args[0]).ToList();

var finder = new EnginePartFinder(entries);
var parts = finder.FindPossibleParts();

var validator = new EnginePartValidator(entries);
var validParts = validator.FilterValidParts(parts);

var partsNumberSum = validParts.Sum(p => p.PartValue);

Console.WriteLine("Sum of valid parts:");
Console.WriteLine(partsNumberSum);