using Slope.Attributes;

namespace Slope.Models;

public record SurfaceLoad
{
    [InputField("Id", 0)]
    [Prompt("Id")]
    [Column(1, "Id")]
    public string Id { get; init; } = "";

    [InputField("q", 1)]
    [Prompt("Intensiy")]
    [Column(2, "q")]
    public double Intensity { get; init; }

    [InputField("Start X", 2)]
    [Prompt("X start")]
    [Column(3, "X_Start")]
    public double XStart { get; init; }

    [InputField("Length", 3)]
    [Prompt("Length")]
    [Column(4, "Id")]
    public double Length { get; init; }
}
