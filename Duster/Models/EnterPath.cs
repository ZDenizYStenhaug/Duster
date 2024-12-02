namespace Duster.Models;

public class EnterPath
{
    public required Coordinate Start { get; init; }
    public required Commands Commands { get; init; }

    /*
     * Returns the result (unique places visited) of running all commands from enterPath
     */
    public int ExecuteCommands()
    {
        var visitedCoordinates = new HashSet<Coordinate> { new() { X = Start.X, Y = Start.Y } };

        var current = new Coordinate { X = Start.X, Y = Start.Y };

        foreach (var command in Commands)
        {
            for (int i = 0; i < command.Steps; i++)
            {
                switch (command.Direction)
                {
                    case Direction.North:
                        current.Y++;
                        break;
                    case Direction.East:
                        current.X++;
                        break;
                    case Direction.South:
                        current.Y--;
                        break;
                    case Direction.West:
                        current.X--;
                        break;
                }
                // Add a copy of the coordinate to ensure immutability in the HashSet
                visitedCoordinates.Add(new Coordinate { X = current.X, Y = current.Y });
            }
        }

        return visitedCoordinates.Count;
    }
}
