using Slope.Metadata.Attributes;

namespace Slope.Models
{
    public record SoilLayer
    {
        [TableField(1, "Id")]
        public string Id { get; init; } = "";

        [TableField(2, "H")]
        public double Height { get; init; }

        [TableField(3, "gama")]
        public double UnitWeight { get; init; }

        [TableField(4, "fi")]
        public double FrictionAngle { get; init; }

        [TableField(5, "c")]
        public double Cohesion { get; init; }
    }
}
