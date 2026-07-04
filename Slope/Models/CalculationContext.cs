using Slope.Helpers;

namespace Slope.Models
{
    public record CalculationContext
    {
        public SlopeGeometry SlopeGeometry { get; set; }
        public SoilLayer Soil { get; set; }
        public List<SurfaceLoad> Actions { get; set; }
    }
}
