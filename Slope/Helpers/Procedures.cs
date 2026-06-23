using Slope.Models;

namespace Slope.Helpers
{
    public class Procedures
    {
        public static Output CalculateFos(
            CalculationContext calculationContext,
            SlipSurface slipSurfase)
        {
            if (!slipSurfase.IsValid(calculationContext.SlopeGeometry))
            {
                return new Output(
                        FOS: double.NaN,
                        SlipSurface: slipSurfase
                    );
            }

            var slipSurfaceDetails = slipSurfase.GetDetails(calculationContext.SlopeGeometry);
            // Weight of the soil mass above the slip surface
            double w = slipSurfaceDetails.Area * calculationContext.Soil.UnitWeight;

            // Active moment
            double m_a = w * (slipSurfase.Centroid.X - slipSurfaceDetails.CentroidX) +
                ActiveMomentFromActions(calculationContext.Actions, calculationContext.SlopeGeometry, slipSurfase);

            // VAR
            double angle1 = Math.Asin(-1 * (slipSurfaceDetails.CentroidX - slipSurfase.Centroid.X) / slipSurfaceDetails.Radius);

            // Passive moment
            double m_p = 
                calculationContext.Soil.Cohesion * slipSurfaceDetails.ArcLength * slipSurfaceDetails.Radius +
                w * Math.Cos(angle1) * Math.Tan(DegreesToRadians(calculationContext.Soil.FrictionAngle)) * slipSurfaceDetails.Radius +
                PasiveMomentFromActions(calculationContext.Actions, calculationContext.SlopeGeometry, slipSurfase, calculationContext.Soil);

            var fos = m_p / m_a;

            return new Output(fos, slipSurfase);
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        private static double ActiveMomentFromActions(List<DistributedLoad> actions, SlopeGeometry slopeGeometry, SlipSurface slipSurfase)
        {
            if (actions == null)
            {
                return default;
            }

            double m_a = default;
            var slipSurfaceDetails = slipSurfase.GetDetails(slopeGeometry);

            foreach (var action in actions)
            {
                if (action == null)
                {
                    return default;
                }

                var intensity = action.Intensity;
                if (action.xStart <= slipSurfaceDetails.Bx)
                {
                    // out of slip surface
                    return default;
                }

                double activeLength = action.xStart - slipSurfaceDetails.Bx;
                if (action.Length > 0 && action.Length < activeLength)
                {
                    activeLength = action.Length;
                }

                double xR = action.xStart - 0.5 * activeLength;

                m_a += intensity * activeLength * (slipSurfase.Centroid.X - xR);
            }

            return m_a;
        }

        private static double PasiveMomentFromActions(List<DistributedLoad> actions, SlopeGeometry slopeGeometry, SlipSurface slipSurfase, SoilLayer soil)
        {
            if (actions == null)
            {
                return default;
            }

            double m_p = default;
            var slipSurfaceDetails = slipSurfase.GetDetails(slopeGeometry);

            foreach(var action in actions )
            {
                if (action == null)
                {
                    continue;
                }

                var intensity = action.Intensity;
                if (action.xStart <= slipSurfaceDetails.Bx)
                {
                    // out of slip surface
                    return default;
                }

                double activeLength = action.xStart - slipSurfaceDetails.Bx;
                if (action.Length > 0 && action.Length < activeLength)
                {
                    activeLength = action.Length;
                }

                double xR = action.xStart - 0.5 * activeLength;

                double angleR = Math.Asin((slipSurfase.Centroid.X - xR) / slipSurfaceDetails.Radius);

                m_p += intensity * activeLength * Math.Cos(angleR) * Math.Tan(DegreesToRadians(soil.FrictionAngle)) * slipSurfaceDetails.Radius;
            }

            return m_p;
        }
    }
}
