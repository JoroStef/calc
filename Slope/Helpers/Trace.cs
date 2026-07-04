using Slope.Models;

namespace Slope.Helpers
{
    public class Trace
    {
        public record MeshPoint(
            double X,
            double Y,
            Output Value);

        public static List<MeshPoint> Start(CalculationContext calculationContext)
        {
            var outputs = new List<Output>();

            var pStart = calculationContext.SlopeGeometry.pA;

            var result = TraceSpiralMesh(pStart.X, pStart.Y, 1.0, f: (x, y) =>
            {
                var centr = new Point2D
                {
                    X = x,
                    Y = y
                };
                var slipSurface = new SlipSurface { Centroid = centr };
                return Procedures.CalculateFos(calculationContext, slipSurface);
            });

            return result;
        }

        public static List<MeshPoint> TraceSpiralMesh(
            double startX,
            double startY,
            double step,
            Func<double, double, Output> f,
            int rounds = 100)
        {
            var result = new List<MeshPoint>();

            double x = startX;
            double y = startY;

            result.Add(new MeshPoint(x, y, f(x, y)));

            int segmentLength = 1;

            for (int round = 0; round < rounds; round++)
            {
                // Right
                for (int i = 0; i < segmentLength; i++)
                {
                    x += step;
                    result.Add(new MeshPoint(x, y, f(x, y)));
                }

                // Up
                for (int i = 0; i < segmentLength; i++)
                {
                    y += step;
                    result.Add(new MeshPoint(x, y, f(x, y)));
                }

                segmentLength++;

                // Left
                for (int i = 0; i < segmentLength; i++)
                {
                    x -= step;
                    result.Add(new MeshPoint(x, y, f(x, y)));
                }

                // Down
                for (int i = 0; i < segmentLength; i++)
                {
                    y -= step;
                    result.Add(new MeshPoint(x, y, f(x, y)));
                }

                segmentLength++;
            }

            return result;
        }

        //public static List<Output> Start(CalculationContext calculationContext)
        //{
        //    var outputs = new List<Output>();

        //    var incX = 1;
        //    var incY = 1;

        //    var _p = calculationContext.SlopeGeometry.pA;
        //    var slipSurface = new SlipSurface { Centroid = _p };
        //    var output = Procedures.CalculateFos(calculationContext, slipSurface);
        //    outputs.Add(output);

        //    bool dirYChanged = false;
        //    do
        //    {
        //        bool dirXChanged = false;
        //        do
        //        {
        //            var newCentroid = new Point2D(_p.X + incX, _p.Y);
        //            var newSlipSurfase = new SlipSurface { Centroid = newCentroid };
        //            var newOutput = Procedures.CalculateFos(calculationContext, newSlipSurfase);

        //            if (newOutput.FOS <= 0)
        //            {
        //                if (!dirXChanged)
        //                {
        //                    // switch direction
        //                    incX = -1;
        //                    dirXChanged = true;
        //                    continue;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            outputs.Add(newOutput);

        //            if (newOutput.FOS > output.FOS)
        //            {
        //                if (!dirXChanged)
        //                {   
        //                    // switch direction
        //                    incX = -1;
        //                    dirXChanged = true;
        //                    continue;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            // continue in the same direction
        //            _p = new Point2D(newSlipSurfase.Centroid.X, _p.Y);
        //            output = newOutput;
        //            slipSurface = newSlipSurfase;
        //        }
        //        while (true);

        //        _p = new Point2D(slipSurface.Centroid.X, _p.Y + incY);
        //        slipSurface = new SlipSurface { Centroid = _p };
        //        var newOutputY = Procedures.CalculateFos(calculationContext, slipSurface);

        //        if (newOutputY.FOS <= 0)
        //        {
        //            if (!dirYChanged)
        //            {
        //                // switch Y direction
        //                incY = -1;
        //                dirYChanged = true;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        outputs.Add(newOutputY);

        //        if (newOutputY.FOS > output.FOS)
        //        {
        //            if (!dirYChanged)
        //            {
        //                // switch Y direction
        //                incY = -1;
        //                dirYChanged = true;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //    } while (true);


        //    return outputs;
        //}
    }
}
