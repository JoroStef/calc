using Slope.Metadata.Attributes;

namespace Slope.Models;

public record Point2D
{
    [TableField(1, "Id")]
    public string Id { get; init; } = "";

    [TableField(1, "X")]
    public double X { get; init; }

    [TableField(2, "Y")]
    public double Y { get; init; }

}
