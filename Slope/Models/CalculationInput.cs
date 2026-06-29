using Slope.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Models
{
    public record CalculationInput
    {
        [InputCollection("Slope geometry", 0)]
        public List<Point2D> Points { get; set; }

        [InputCollection("Layers", 1)]
        public List<SoilLayer> Layers { get; set; }

        [InputCollection("Loads", 2)]
        public List<DistributedLoad> Loads { get; set; }
    }
}
