using Slope.Models;
using Slope.Report.Contracts;

namespace Slope.Report.Models
{
    public class SvgLine : ISvgElement
    {
        public SvgLine(
            double x1,
            double y1,
            double x2,
            double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public SvgLine(Point2D p1, Point2D p2)
        {
            X1 = p1.X;
            Y1 = p1.Y;
            X2 = p2.X;
            Y2 = p2.Y;
        }

        public double X1 { get; }
        public double Y1 { get; }
        public double X2 { get; }
        public double Y2 { get; }

        public string ToSvg()
        {
            return
                $"<line x1='{X1}' y1='{Y1}' x2='{X2}' y2='{Y2}' stroke='black'/>";
        }
    }
}
