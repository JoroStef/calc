using Slope.Attributes;

namespace Slope.Models
{
    public record CalculationInput
    {
        [InputCollection("Slope geometry", 0)]
        public List<Point2D> Geometry { get; set; } = new List<Point2D>();

        [InputCollection("Layers", 1)]
        public List<SoilLayer> Layers { get; set; } = new List<SoilLayer>();

        [InputCollection("Loads", 2)]
        public List<SurfaceLoad> Loads { get; set; } = new List<SurfaceLoad>();
    }
}
