using Slope.Attributes;

namespace Slope.Models;

public record Point2D
{
    [InputField("Id", 0)]
    [Prompt("Id")]
    [Column(1, "Id")]
    public string Id { get; init; } = "";

    [InputField("X", 0)]
    [Prompt("X")]
    [Column(1, "X")]
    public double X { get; init; }

    [InputField("Y", 1)]
    [Prompt("Y")]
    [Column(2, "Y")]
    public double Y { get; init; }

}
