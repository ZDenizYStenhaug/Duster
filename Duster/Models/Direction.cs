using System.Text.Json.Serialization;

namespace Duster.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Direction { North, East, South, West }