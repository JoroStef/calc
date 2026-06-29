using Slope.Attributes;

namespace Slope.Models
{
    public record DistributedLoad
    {
        [InputField("Id", 0)]
        public string Id { get; init; } = "";

        [InputField("q", 1)]
        public double Intensity { get; init; }

        [InputField("Start X", 2)]
        public double XStart { get; init; }

        [InputField("Length", 3)]
        public double Length { get; init; }
    }

}
