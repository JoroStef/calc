using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Models
{
    public record Input
    {
        public List<Point2D> Points { get; set; }
        public List<SoilLayer> Layers { get; set; }
        public List<DistributedLoad> Loads { get; set; }
    }
}
