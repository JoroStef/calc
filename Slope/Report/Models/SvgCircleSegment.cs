using Slope.Models;
using Slope.Report.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Report.Models
{
    public class SvgCircleSegment : ISvgElement
    {
        public SvgCircleSegment(Point2D center, double Radius)
        {
            Xc = center.X;
            Yc = center.Y;
            R = Radius;
        }

        public double Xc { get; set; }
        public double Yc { get; set; }
        public double R { get; set; }

        public SvgBounds GetBounds()
        {
            throw new NotImplementedException();
        }

        public string ToSvg()
        {
            return $"<circle cx='{Xc}' cy='{Yc}' r='{R}' fill='none' stroke='black'/>";
        }
    }
}
