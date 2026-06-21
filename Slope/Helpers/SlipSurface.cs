namespace Slope.Helpers
{
    using Slope.Models;
    using System;

    public record CircularWedgeResult(
        double Area,
        double ArcLength,
        double Radius,
        double CentroidX,
        double CentroidY,
        double Bx,
        double By,
        // Towards positive X
        double StartAngle,
        double EndAngle);

    public class SlipSurface
    {
        private CircularWedgeResult? _details;

        public Point2D Centroid { get; set; }

        public bool IsValid(SlopeGeometry slopeGeometry)
        {
            double x0 = Centroid.X;
            double y0 = Centroid.Y;
            double xA = slopeGeometry.pA.X;
            double yA = slopeGeometry.pA.Y;
            double R = Math.Sqrt(x0 * x0 + y0 * y0);

            double rA = Math.Sqrt(Math.Pow(x0 - xA, 2) + Math.Pow(y0 - yA, 2));

            return rA < R;
        }

        public CircularWedgeResult GetDetails(SlopeGeometry slopeGeometry)
        {
            if (_details != null)
            {
                return _details;
            }
            double x0 = Centroid.X;
            double y0 = Centroid.Y;
            double xA = slopeGeometry.pA.X;
            double yA = slopeGeometry.pA.Y;
            double R = Math.Sqrt(x0 * x0 +  y0 * y0);

            // -----------------------------
            // Check precondition
            // -----------------------------
            if (!IsValid(slopeGeometry))
            {
                _details = new CircularWedgeResult(
                    double.NaN,
                    double.NaN,
                    R,
                    double.NaN,
                    double.NaN,
                    double.NaN,
                    double.NaN,
                    double.NaN,
                    double.NaN);

                return _details;
            }

            // -----------------------------
            // Point B
            // -----------------------------

            double dy = yA - y0;

            double bx = x0 - Math.Sqrt(R * R - dy * dy);

            double by = yA;

            // -----------------------------
            // Central angle
            // -----------------------------

            double ux = -x0;
            double uy = -y0;

            double vx = bx - x0;
            double vy = by - y0;

            double dot = ux * vx + uy * vy;
            double cross = ux * vy - uy * vx;

            double theta = Math.Atan2(Math.Abs(cross), dot);

            // Arc used by the contour
            double phi = cross < 0 ? theta : 2.0 * Math.PI - theta;

            // -----------------------------
            // Triangle PAB
            // -----------------------------

            double areaTriangle = 0.5 * yA * (xA - bx);

            double xt = (xA + bx) / 3.0;

            double yt = 2.0 * yA / 3.0;

            // -----------------------------
            // Circular segment
            // -----------------------------

            double areaSegment = 0.5 * R * R * (phi - Math.Sin(phi));

            // chord midpoint
            double mx = bx / 2.0;
            double my = by / 2.0;

            // centroid distance from circle center
            double ds =
                4.0 * R *
                Math.Pow(Math.Sin(phi / 2.0), 3.0) /
                (3.0 * (phi - Math.Sin(phi)));

            // direction O -> midpoint of chord
            double nx = mx - x0;
            double ny = my - y0;

            double len = Math.Sqrt(nx * nx + ny * ny);

            nx /= len;
            ny /= len;

            double xs = x0 + ds * nx;

            double ys = y0 + ds * ny;

            // -----------------------------
            // Composite area
            // -----------------------------

            double area =
                areaTriangle + areaSegment;

            double xg =
                (areaTriangle * xt +
                 areaSegment * xs) / area;

            double yg =
                (areaTriangle * yt +
                 areaSegment * ys) / area;

            double arcLength = R * phi;

            double startAngle = Math.Atan2(0 - y0, 0 - x0);
            startAngle = NormalizeAngle(startAngle);
            
            double endAngle = Math.Atan2(by - y0, bx - x0);
            endAngle = NormalizeAngle(endAngle);

            _details = new CircularWedgeResult(
                area,
                arcLength,
                R,
                xg,
                yg,
                bx,
                by,
                startAngle,
                endAngle);

            return _details;
        }

        public static double NormalizeAngle(double angle)
        {
            while (angle < 0)
                angle += 2 * Math.PI;

            while (angle >= 2 * Math.PI)
                angle -= 2 * Math.PI;

            return angle;
        }
    }
}
