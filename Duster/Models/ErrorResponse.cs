namespace Duster.Models;

public class ErrorResponse
{
    public required string  Message { get; init; }
    public string? Details { get; init; }
}