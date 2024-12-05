using Duster.Models;

namespace Tests;

public class EnterPathUnitTest
{
    [Fact]
    public void TestExecuteCommands_SimpleCase()
    {
        // Arrange
        var enterPath = new EnterPath
        {
            Start = new Coordinate { X = 10, Y = 22 },
            Commands =
            [
                new Command { Direction = Direction.East, Steps = 2 },
                new Command { Direction = Direction.North, Steps = 1 }
            ]
        };
        var expected = 4;
        
        // Act
        var actual = enterPath.ExecuteCommands();
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestExecuteCommands_OverlappingDirections()
    {
        // Arrange
        var enterPath = new EnterPath
        {
            Start = new Coordinate { X = 5, Y = 10 },
            Commands =
            [
                new Command { Direction = Direction.West, Steps = 5 },
                new Command { Direction = Direction.South, Steps = 3 },
                new Command { Direction = Direction.East, Steps = 4 },
                new Command { Direction = Direction.North, Steps = 4},
                new Command { Direction = Direction.East, Steps = 2},
                new Command { Direction = Direction.South, Steps = 3},
            ]
        };
        var expected = 21;
        
        // Act
        var actual = enterPath.ExecuteCommands();
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestExecuteCommands_MultipleOverlappingDirections()
    {
        // Arrange
        var enterPath = new EnterPath
        {
            Start = new Coordinate { X = 5, Y = -20 },
            Commands =
            [
                new Command { Direction = Direction.West, Steps = 6 },
                new Command { Direction = Direction.East, Steps = 5 },
                new Command { Direction = Direction.West, Steps = 4 },
                new Command { Direction = Direction.East, Steps = 3 }
            ]
        };
        var expected = 7;
        
        // Act
        var actual = enterPath.ExecuteCommands();
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestExecuteCommands_EmptyCommandsList()
    {
        // Arrange
        var enterPath = new EnterPath
        {
            Start = new Coordinate { X = 0, Y = 0 },
            Commands = []
        };
        var expected = 1;
        
        // Act
        var actual = enterPath.ExecuteCommands();
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestExecuteCommands_HeavyCase()
    {
        // Arrange 
        var commands = new Commands();
        for (int i = 0; i < 25000; i++)
        {
            Direction direction = (Direction)(i % 4);
            commands.Add(new Command { Direction = direction, Steps = direction is Direction.North or Direction.East? (uint)99999 : 99998 });
        }
        
        var enterPath = new EnterPath
        {
            Start = new Coordinate { X = 0, Y = 0 },
            Commands = commands
        };
        var timeout = 10; 
        
        // Act
        var startTime = DateTime.Now;
        var actual = enterPath.ExecuteCommands();
        var endTime = DateTime.Now;
        
        Assert.True(endTime.Subtract(startTime).TotalSeconds < timeout);
    }
}