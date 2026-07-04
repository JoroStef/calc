using Slope.Attributes;
using Slope.Models.UiEditors;
using System.Reflection;

namespace Slope.Models
{
    public record SoilLayer : Slope.Models.IFormattable
    {
        [InputField("Id", 0)]
        [Prompt("Id")]
        [Column(1, "Id")]
        public string Id { get; init; } = "";

        [InputField("Height", 1)]
        [Prompt("Height (m)")]
        [Column(2, "H")]
        public double Height { get; init; }

        [InputField("γ", 2)]
        [Prompt("Unit weight (kN/m3)")]
        [Column(3, "gama")]
        public double UnitWeight { get; init; }

        [InputField("φ", 3)]
        [Prompt("Friction angle (drgr.)")]
        [Column(4, "fi")]
        public double FrictionAngle { get; init; }

        [InputField("c", 4)]
        [Prompt("Cohesion (kPa)")]
        [Column(5, "c")]
        public double Cohesion { get; init; }

        public string Format()
        {
            return
                $"{this.Id,-10}" +
                $"{this.Height,8:F2}" +
                $"{this.UnitWeight,8:F2}" +
                $"{this.FrictionAngle,8:F1}" +
                $"{this.Cohesion,8:F1}";
        }
    }
}
