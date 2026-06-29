using Slope.Attributes;

namespace Slope.Models
{
    public record SoilLayer
    {
        [InputField("Id", 0)]
        public string Id { get; init; } = "";

        [InputField("Height", 1)]
        public double Height { get; init; }

        [InputField("γ", 2)]
        public double UnitWeight { get; init; }

        [InputField("φ", 3)]
        public double FrictionAngle { get; init; }

        [InputField("c", 4)]
        public double Cohesion { get; init; }
    }
}
