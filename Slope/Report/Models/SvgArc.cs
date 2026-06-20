using Slope.Models;
using Slope.Report.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Report.Models
{
    public class SvgArc : ISvgElement
    {
        public Point2D Center { get; }
        public double Radius { get; }

        /// angle in radians
        public double StartAngle { get; }

        /// angle in radians
        public double EndAngle { get; }

        public string Stroke { get; set; } = "black";
        public double StrokeWidth { get; set; } = 1;
        public bool Fill { get; set; } = false;

        public SvgArc(Point2D center, double radius, double startAngle, double endAngle)
        {
            Center = center;
            Radius = radius;
            StartAngle = startAngle;
            EndAngle = endAngle;
        }

        public string ToSvg()
        {
            var start = ToPoint(StartAngle);
            var end = ToPoint(EndAngle);

            var largeArc = (Math.Abs(EndAngle - StartAngle) > Math.PI) ? 1 : 0;

            // sweep-flag = 1 means clockwise in SVG
            var sweep = EndAngle > StartAngle ? 1 : 0;

            return
    $"""
<path d="M {start.X} {start.Y}
         A {Radius} {Radius} 0 {largeArc} {sweep} {end.X} {end.Y}"
      fill="none"
      stroke="{Stroke}"
      stroke-width="{StrokeWidth}" />
""";
        }

        private Point2D ToPoint(double angle)
        {
            return new Point2D(
                Center.X + Radius * Math.Cos(angle),
                Center.Y + Radius * Math.Sin(angle)
            );
        }
    }
}
