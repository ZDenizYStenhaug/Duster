namespace Duster.Models;

public class Coordinate
{
    public required int X { get; set; }
    public required int Y { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Coordinate other) return false;
        return X == other.X && Y == other.Y;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}