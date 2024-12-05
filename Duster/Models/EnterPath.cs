namespace Duster.Models;

public class EnterPath
{
    public required Coordinate Start { get; init; }
    public required Commands Commands { get; init; }

    private struct Range(int start, int end)
    {
        public int Start { get; set; } = start;
        public int End { get; set; } = end;
    }
    
    // Returns the result (unique places visited) of running all commands from enterPath
    public int ExecuteCommands()
    {
        // save the ranges for vertical and horizontal movements
        Dictionary<int, Range> horizontalRanges = new Dictionary<int, Range>();
        Dictionary<int, Range> verticalRanges = new Dictionary<int, Range>();
        
        AddRange(verticalRanges, Start.X, new Range(Start.Y, Start.Y)); // add start position (enough to add it to only one range)
        
        var currentCoordinate = new Coordinate { X = Start.X, Y = Start.Y };
        foreach (var command in Commands)
        {
            Coordinate endCoordinate = Move(currentCoordinate, command);
            Coordinate firstStepCoordinate = Move(currentCoordinate, new Command {Direction = command.Direction, Steps = 1});
            if (command.Direction is Direction.North or Direction.South)
                AddRange(verticalRanges, currentCoordinate.X, GetRange(firstStepCoordinate, endCoordinate, command.Direction));
            else
                AddRange(horizontalRanges, currentCoordinate.Y, GetRange(firstStepCoordinate, endCoordinate, command.Direction));
            currentCoordinate = endCoordinate;
        }
        // count unique coordinates
        int uniqueCoordinatesVisited = CountAllUniqueCoordinates(horizontalRanges, verticalRanges);
        
        return uniqueCoordinatesVisited;
    }
    
    // Ranges are always defined as from west to east, and from south to north in order to ease comparison in AddRange method.
    private Range GetRange(Coordinate startCoordinate, Coordinate endCoordinate, Direction direction)
    {
        if (direction is Direction.North or Direction.South) {
            if (startCoordinate.Y < endCoordinate.Y)
            {
                return new Range(startCoordinate.Y, endCoordinate.Y);
            } 
            return new Range(endCoordinate.Y, startCoordinate.Y);
        }
        if (startCoordinate.X < endCoordinate.X)
        {
            return new Range(startCoordinate.X, endCoordinate.X);
        } 
        return new Range(endCoordinate.X, startCoordinate.X);
    }

    private Coordinate Move(Coordinate startCoordinate, Command command)
    {
        Coordinate endCoordinate = new() { X = startCoordinate.X, Y = startCoordinate.Y };
        switch (command.Direction)
        {
            case Direction.North: 
                endCoordinate.Y += (int) command.Steps; break;
            case Direction.East: 
                endCoordinate.X += (int) command.Steps; break;
            case Direction.South:
                endCoordinate.Y -= (int)command.Steps; break;
            case Direction.West: 
                endCoordinate.X -= (int) command.Steps; break;
        }
        return endCoordinate;
    }

    private void AddRange(Dictionary<int, Range> ranges, int key, Range range)
    {
        if (!ranges.ContainsKey(key))
            ranges[key] = range;
        Range existingRange = ranges[key];
        if (range.Start <  ranges[key].Start)
            existingRange.Start = range.Start;
        if (range.End > ranges[key].End)
            existingRange.End = range.End;
        ranges[key] = existingRange;
    }
    
    private int CountAllUniqueCoordinates(Dictionary<int, Range> horizontalRanges, Dictionary<int, Range> verticalRanges)
    {
        var intersections = 0;
        int visitedCoordinatesCount = 0;
        foreach (int vKey in verticalRanges.Keys)
        {
            visitedCoordinatesCount += Math.Abs(verticalRanges[vKey].Start - verticalRanges[vKey].End) + 1;
            foreach (int hKey in horizontalRanges.Keys)
            {
                if (vKey >= horizontalRanges[hKey].Start && vKey <= horizontalRanges[hKey].End
                    && hKey >= verticalRanges[vKey].Start && hKey <= verticalRanges[vKey].End)
                    intersections++;
            }
        }

        foreach (int hKey in horizontalRanges.Keys)
            visitedCoordinatesCount += Math.Abs(horizontalRanges[hKey].Start - horizontalRanges[hKey].End) + 1;
        
        return visitedCoordinatesCount - intersections;
    }
}
