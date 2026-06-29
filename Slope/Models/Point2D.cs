using Slope.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Models
{
    public readonly record struct Point2D
        (
            [property: InputField("X", 0)]
            double X,

            [property: InputField("Y", 1)]
            double Y
        );
}
