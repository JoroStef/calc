using Slope.Metadata.Attributes;

namespace Slope.Models;

public record SurfaceLoad
{
    [TableField(1, "Id")]
    public string Id { get; init; } = "";

    [TableField(2, "q")]
    public double Intensity { get; init; }

    [TableField(3, "X_Start")]
    public double XStart { get; init; }

    [TableField(4, "Id")]
    public double Length { get; init; }
}
