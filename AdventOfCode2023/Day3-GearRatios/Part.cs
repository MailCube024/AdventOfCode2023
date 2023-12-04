namespace Day3_GearRatios;

public record Part(string PartNumber, Coordinates Position)
{
    public int PartLength => PartNumber.Length;
    public int PartValue => int.Parse(PartNumber);
}