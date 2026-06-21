using Slope.Models;
using Slope.Report.Contracts;
using Slope.Report.Models;

public class SvgArc : ISvgElement
{
    public Point2D Center { get; init; }

    public Point2D Start { get; init; }

    public Point2D End { get; init; }

    public double Radius { get; init; }

    public string Stroke { get; set; } = "black";

    public double StrokeWidth { get; set; } = 0.05;

    public SvgArc(
        Point2D center,
        Point2D start,
        Point2D end,
        double radius)
    {
        Center = center;
        Start = start;
        End = end;
        Radius = radius;
    }

    public string ToSvg()
    {
        double startAngle = Math.Atan2(
            Start.Y - Center.Y,
            Start.X - Center.X);

        double endAngle = Math.Atan2(
            End.Y - Center.Y,
            End.X - Center.X);

        // Normalize to [0, 2π)
        if (startAngle < 0)
            startAngle += 2 * Math.PI;

        if (endAngle < 0)
            endAngle += 2 * Math.PI;

        // Cross product determines orientation
        double cross =
            (Start.X - Center.X) * (End.Y - Center.Y)
          - (Start.Y - Center.Y) * (End.X - Center.X);

        bool clockwise = cross < 0;

        double ccwAngle = endAngle - startAngle;

        if (ccwAngle < 0)
            ccwAngle += 2 * Math.PI;

        double cwAngle = 2 * Math.PI - ccwAngle;

        double arcAngle =
            clockwise
                ? cwAngle
                : ccwAngle;

        bool largeArc = arcAngle > Math.PI;

        int largeArcFlag = largeArc ? 1 : 0;
        int sweepFlag = clockwise ? 0 : 1;

        return
    $"""
<path
    d="M {Start.X} {Start.Y}
       A {Radius} {Radius}
         0
         {largeArcFlag}
         {sweepFlag}
         {End.X} {End.Y}"
    fill="none"
    stroke="{Stroke}"
    stroke-width="{StrokeWidth}" />
""";
    }

    public SvgBounds GetBounds()
    {
        // TODO: This is not mathematically perfect (true arc extrema can lie in the middle), but works for most engineering drawings.

        double minX = Math.Min(Start.X, End.X);
        double minY = Math.Min(Start.Y, End.Y);
        double maxX = Math.Max(Start.X, End.X);
        double maxY = Math.Max(Start.Y, End.Y);

        return new SvgBounds(minX, minY, maxX, maxY);
    }
}