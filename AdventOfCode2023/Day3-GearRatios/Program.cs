// See https://aka.ms/new-console-template for more information

using Day3_GearRatios;

var entries = File.ReadLines(args[0]);

var engine = new EngineMap(entries.ToList());
var parts = engine.FindPossibleParts().ToList();

var partsNumberSum = 0;

Console.WriteLine("Sum of valid parts:");
Console.WriteLine(partsNumberSum);