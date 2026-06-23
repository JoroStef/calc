using Slope.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Helpers
{
    public record SlopeGeometry
        (
            Point2D pA
        );

    public record PointsCoordinates
    {
        public List<Point2D> Points { get; set; }
    }

}
