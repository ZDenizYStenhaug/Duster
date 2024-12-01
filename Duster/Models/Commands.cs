namespace Duster.Models;


public class Commands : List<Command>
{
}

public class Command
{
    public required Direction Direction { get; set; }
    public required uint Steps { get; set; }
}
