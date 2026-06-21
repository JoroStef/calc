namespace Slope.Report.Models
{
    public readonly struct SvgBounds
    {
        public double MinX { get; }
        public double MinY { get; }
        public double MaxX { get; }
        public double MaxY { get; }

        public SvgBounds(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        public static SvgBounds Empty =>
            new(double.PositiveInfinity,
                double.PositiveInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity);

        public SvgBounds Expand(SvgBounds other)
        {
            return new SvgBounds(
                Math.Min(MinX, other.MinX),
                Math.Min(MinY, other.MinY),
                Math.Max(MaxX, other.MaxX),
                Math.Max(MaxY, other.MaxY));
        }
    }
}
